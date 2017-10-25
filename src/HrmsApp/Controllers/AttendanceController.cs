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
    public class AttendanceController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;

        public AttendanceController(HrmsDbContext context, ILookupServices lookup)
        {
            _context = context;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AttendancesList()
        {
            var model = _context.Attendances.Where(b => b.IsActive);
            return PartialView("_AttendancesList", await model.ToListAsync());
        }

        public IActionResult AddAttendance()
        {
            return PartialView("_AddAttendance");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAttendance(Attendance item)
        {
            await _context.Attendances.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("AttendancesList");
        }

        public async Task<IActionResult> EndorseAttendance(long id)
        {
            var model = await _context.Attendances.SingleOrDefaultAsync(b => b.Id == id);
            model.IsActive = false;
            model.IsEndorsed = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("AttendancesList");
        }

        //details
        public async Task<IActionResult> AttendanceDetails(long id)
        {
            List<EmployeeAttendanceViewModel> model = new List<EmployeeAttendanceViewModel>();
            var x = await _context.EmployeeAttendances.Include(b => b.Employee).Include(b => b.AttendanceActionType).Include(b => b.PayrollComponentType)
                            .Where(b => b.AttendanceId == id).ToListAsync();
            foreach(var emp in x.Select(b => b.Employee).Distinct())
            {
                EmployeeAttendanceViewModel item = new EmployeeAttendanceViewModel();
                item.Id = x.SingleOrDefault(b => b.EmployeeId == emp.Id && b.PayrollComponentType.SysCode == "BASIC").Id;
                item.EmployeeId = emp.Id;
                item.EmployeeName = emp.FirstName + " " + emp.FatherName + " " + emp.FamilyName;
                item.RequiredMinutes = x.SingleOrDefault(b => b.EmployeeId == emp.Id && b.PayrollComponentType.SysCode == "BASIC").RequiredMinutes;
                item.Overtime1Minutes = 0;
                item.Overtime2Minutes = 0;
                item.WeekendMinutes = 0;
                var x1 = x.SingleOrDefault(b => b.EmployeeId == emp.Id && b.PayrollComponentType.SysCode == "OT1");
                if (x1 != null)
                    item.Overtime1Minutes = x1.TotalMinutes;
                x1 = x.SingleOrDefault(b => b.EmployeeId == emp.Id && b.PayrollComponentType.SysCode == "OT2");
                if (x1 != null)
                    item.Overtime2Minutes = x1.TotalMinutes;
                x1 = x.SingleOrDefault(b => b.EmployeeId == emp.Id && b.PayrollComponentType.SysCode == "WEEKEND");
                if (x1 != null)
                    item.WeekendMinutes = x1.TotalMinutes;
                item.CompliancePercentage = x.SingleOrDefault(b => b.EmployeeId == emp.Id && b.PayrollComponentType.SysCode == "BASIC").CompliancePercentage;
                item.AttendanceActionTypeId = x.SingleOrDefault(b => b.EmployeeId == emp.Id && b.PayrollComponentType.SysCode == "BASIC").AttendanceActionTypeId;
                item.AttendanceActionTypeName = x.SingleOrDefault(b => b.EmployeeId == emp.Id && b.PayrollComponentType.SysCode == "BASIC").AttendanceActionType.Name;
                model.Add(item);
            }
            return PartialView("_AttendanceDetails", model);
        }

        public async Task<IActionResult> EditEmployeeAttendance(long id)
        {
            var model = await _context.EmployeeAttendances.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.attendanceActionTypesList = _lookup.GetLookupItems<AttendanceActionType>();
            return PartialView("_EditEmployeeAttendance", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployeeAttendance(EmployeeAttendance item)
        {
            var model = await _context.EmployeeAttendances.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("AttendanceDetails", new { id = model.AttendanceId });
        }
    }
}