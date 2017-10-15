using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HrmsModel.Models;
using HrmsModel.Data;
using Microsoft.EntityFrameworkCore;

namespace HrmsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HrmsDbContext _context;
        public HomeController(HrmsDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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
    }
}
