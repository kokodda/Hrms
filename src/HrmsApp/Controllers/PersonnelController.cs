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
    public class PersonnelController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;

        public PersonnelController(HrmsDbContext context, ILookupServices lookup)
        {
            _context = context;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            ViewBag.orgUnitsList = _context.OrgUnits.Include(b => b.OrgUnitType)
                        .Where(b => b.IsActive).OrderBy(b => b.OrgUnitType.SortOrder).ThenBy(b => b.SortOrder).ToList();
            ViewBag.nationalitiesList = _context.Nationalities.Where(b => b.IsActive).ToList();
            ViewBag.governoratesList = _context.Governorates.Where(b => b.IsActive).ToList();
            return View();
        }

        public async Task<IActionResult> EmployeesList(long? ouId)
        {
            var model = _context.Employees.Include(b => b.EmployeePositions).ThenInclude(b => b.OrgUnit).Include(b => b.Governorate).Include(b => b.Nationality)
                                .Where(b => !ouId.HasValue || b.EmployeePositions.FirstOrDefault(c => c.IsActive && !c.IsActing).OrgUnitId == ouId);
            return PartialView("_EmployeesList", await model.ToListAsync());
        }

        public async Task<IActionResult> EmployeeIndex(long id)
        {
            var model = _context.Employees.Include(b => b.EmployeePositions).ThenInclude(b => b.OrgUnit).ThenInclude(b => b.JobGrade)
                                .Include(b => b.EmployeePositions).ThenInclude(b => b.SalaryScale)
                                .Include(b => b.EmployeePositions).ThenInclude(b => b.SalaryStep)
                                .Include(b => b.EmployeePositions).ThenInclude(b => b.EmployeeType)
                                .Include(b => b.Nationality)
                                .SingleOrDefaultAsync(b => b.Id == id);
            return View("EmployeeIndex", await model);
        }

        public async Task<IActionResult> LeaveBalancesList(long id)
        {
            var model = _context.EmployeeLeaveBalances.Include(b => b.LeaveType).Where(b => b.EmployeeId == id);
            return PartialView("_LeaveBalancesList", await model.ToListAsync());
        }

        public async Task<IActionResult> LeavesList(long id)
        {
            var model = _context.EmployeeLeaves.Include(b => b.LeaveType).Where(b => b.EmployeeId == id && b.ThruDate.Year == DateTime.Now.Year)
                                                .OrderByDescending(b => b.FromDate);
            ViewBag.employeeId = id;
            return PartialView("_LeavesList", await model.ToListAsync());
        }

        public IActionResult AddLeave(long id)
        {
            ViewBag.leaveTypesList = _lookup.GetLookupItems<LeaveType>();
            return PartialView("_AddLeave");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLeave(EmployeeLeave item)
        {
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.EmployeeLeaves.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("LeavesList", item.EmployeeId);
        }
    }
}