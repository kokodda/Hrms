using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HrmsModel.Models;
using HrmsModel.Data;
using HrmsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HrmsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;

        public HomeController(HrmsDbContext context, ILookupServices lookup)
        {
            _context = context;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Carousel()
        {
            var model = _context.CarouselItems.Where(b => b.IsActive).OrderBy(b => b.SortOrder);
            return PartialView("_Carousel", await model.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> HolidaysList()
        {
            List<string> model = new List<string>();
            var x = await _context.Holidays.Where(b => b.IsActive).OrderBy(b => b.FromMonth).ThenBy(b => b.FromDay).ToListAsync();
            DateTime hDate = DateTime.Today;
            var y = await _context.HolidayVariations.Where(b => b.FromDate.Year == hDate.Year && b.IsActive).ToListAsync();
            string dateString;
            foreach (var x1 in x)
            {
                if(!x1.IsHijri)
                {
                    hDate = new DateTime(DateTime.Today.Year, x1.FromMonth, x1.FromDay);
                    if (hDate > DateTime.Today.Date)
                    {
                        dateString = x1.FromDay + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x1.FromMonth);
                        model.Add(x1.Name + "|" + dateString + " - " + x1.NbrDays + " days");
                    }
                }
                else
                {
                    var y1 = y.SingleOrDefault(b => b.HolidayId == x1.Id); //check for variation for hijri holidays
                    if (y1 != null)
                    {
                        if(y1.FromDate.Date >= DateTime.Today.Date)
                        {
                            dateString = y1.FromDate.Day + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(y1.FromDate.Month);
                            model.Add(x1.Name + "|" + dateString + " - " + y1.NbrDays + " days");
                        }
                    }
                    else
                    {
                        dateString = x1.FromDay + "/" + x1.FromMonth;
                        model.Add(x1.Name + "|" + dateString + " (Hijri)");
                    }
                }
            }
            return PartialView("_HolidaysList", model);
        }

        public async Task<IActionResult> LeaveList()
        {
            List<string> model = new List<string>();
            var x = await _context.EmployeeLeaves.Include(b => b.LeaveType).Where(b => b.FromDate.Date == DateTime.Today.Date && !b.IsCancelled)
                                                .OrderBy(b => b.LeaveType.SortOrder).ToListAsync();
            int cnt = 0;
            int totalLeave = 0;
            foreach (var x1 in x.Select(b => b.LeaveType).Distinct())
            {
                cnt = x.Where(b => b.LeaveTypeId == x1.Id).Count();
                totalLeave += cnt;
                model.Add(x1.Name + "|" + cnt.ToString());
            }
            int totalPersonnel = await _context.Employees.Where(b => b.IsActive).CountAsync();
            ViewBag.absencePctg = totalLeave * 100 / totalPersonnel;
            return PartialView("_LeaveList", model);
        }

        public async Task<IActionResult> LeaveDetails()
        {
            var model = await _context.EmployeeLeaves.Include(b => b.Employee).ThenInclude(b => b.Employments).ThenInclude(b => b.OrgUnit)
                                        .Where(b => b.FromDate == DateTime.Today.Date && !b.IsCancelled).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Welcome()
        {
            List<EmployeeViewModel> model = new List<EmployeeViewModel>();
            var x = await _context.Employments.Include(b => b.Employee).Include(b => b.OrgUnit).Include(b => b.Position)
                                    .Where(b => b.IsActive).Take(5).ToListAsync();
            foreach(var x1 in x)
            {
                EmployeeViewModel item = new EmployeeViewModel();
                item.Employee = x1.Employee;
                item.EmployeeName = x1.Employee.FirstName + " " + x1.Employee.FatherName + " " + x1.Employee.FamilyName;
                item.OrgUnitName = x1.OrgUnit.Name;
                item.PositionName = x1.PositionId.HasValue ? x1.Position.Name : (x1.IsHead ? x1.OrgUnit.HeadPositionName : x1.JobName);
                model.Add(item);
            }
            return PartialView("_Welcome", model);
        }

        public async Task<IActionResult> EditContacts()
        {
            var model = await _context.Employees.FirstOrDefaultAsync(b => b.IsActive);
            ViewBag.governoratesList = await _context.Governorates.Where(b => b.IsActive).ToListAsync();
            return View("EditContacts", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContacts(Employee item)
        {
            var model = await _context.Employees.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";
            await _context.SaveChangesAsync();

            return Content("Thank You for Updating Your Contacts.", "text/html");
        }

        public IActionResult AddLeaveRequest()
        {
            ViewBag.leaveTypesList = _lookup.GetLookupItems<LeaveType>();
            return View("AddLeaveRequest");
        }

        public async Task<IActionResult> Library()
        {
            var model = _context.SiteContents.Where(b => b.IsActive).OrderByDescending(b => b.LastUpdated);
            return View(await model.ToListAsync());
        }

        public async Task<IActionResult> HomeLibrary()
        {
            var model = _context.SiteContents.Where(b => b.ContentType == "Document" && b.IsActive).OrderByDescending(b => b.LastUpdated).Take(3);
            return PartialView("_HomeLibrary", await model.ToListAsync());
        }
    }
}
