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
    public class PayrollsController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;

        public PayrollsController(HrmsDbContext context, ILookupServices lookup)
        {
            _context = context;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PayrollsList()
        {
            var model = _context.Payrolls.Include(b => b.SalaryScale).Include(b => b.Attendance);
            return PartialView("_PayrollsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddPayroll()
        {
            ViewBag.salaryScalesList = await _context.SalaryScales.Where(b => b.IsActive).ToListAsync();
            return PartialView("_AddPayroll");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPayroll(Payroll item)
        {
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.Payrolls.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("PayrollsList");
        }

        public async Task<IActionResult> UpdatePayroll(long id, string newStatus)
        {
            var item = await _context.Payrolls.SingleOrDefaultAsync(b => b.Id == id);
            switch (newStatus)
            {
                case "Approve":
                    item.IsApproved = true;
                    break;
                case "Cancel":
                    item.IsActive = false;
                    break;
                case "Endorse":
                    item.IsEndorsed = true; item.IsActive = false;
                    break;
                case "Export":
                    item.IsExported = true;
                    break;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("PayrollsList");
        }

        public async Task<IActionResult> LinkAttendance()
        {
            var model = await _context.Attendances.Where(b => b.IsEndorsed).ToListAsync();
            return PartialView("_LinkAttendance", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkAttendance(long payrollId, long attendanceId)
        {
            var model = await _context.Payrolls.SingleOrDefaultAsync(b => b.Id == payrollId);
            model.AttendanceId = attendanceId;
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";
            await _context.SaveChangesAsync();
            return RedirectToAction("PayrollsList");
        }

        //payroll additions
        public async Task<IActionResult> PayrollAdditionsList(long id)
        {
            var model = _context.PayrollAdditions.Include(b => b.PayrollComponentType).Include(b => b.Employee)
                                .Include(b => b.GradeGroup).Include(b => b.FromJobGrade).Include(b => b.ThruJobGrade)
                                .Where(b => b.PayrollId == id);
            return PartialView("_PayrollAdditionsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddPayrollAddition(string level)
        {
            ViewBag.payrollComponentTypesList = _lookup.GetLookupItems<PayrollComponentType>();
            switch(level)
            {
                case "INDIVIDUAL":
                    ViewBag.employeesList = await _context.Employees.Where(b => b.IsActive).ToListAsync();
                    return PartialView("_AddPayrollEmployeeAddition");
                case "GRADE_GROUP":
                    ViewBag.gradeGroupsList = await _context.GradeGroups.Where(b => b.IsActive).ToListAsync();
                    return PartialView("_AddPayrollGroupAddition");
                case "JOB_GRADE":
                    ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).ToListAsync();
                    return PartialView("_AddPayrollGradeAddition");
                default:
                    return PartialView("_AddPayrollCompanyAddition");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPayrollAddition(PayrollAddition item)
        {
            _context.PayrollAdditions.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("PayrollAdditionsList", new { id = item.PayrollId });
        }

        public async Task<IActionResult> DeletePayrollAddition(long id)
        {
            var item = _context.PayrollAdditions.SingleOrDefault(b => b.Id == id);
            long prId = item.PayrollId;
            _context.PayrollAdditions.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("PayrollAdditionsList", new { id = prId });
        }

        //payroll deductions
        public async Task<IActionResult> PayrollDeductionsList(long id)
        {
            var model = _context.PayrollDeductions.Include(b => b.Employee).Include(b => b.PayrollComponentType).Where(b => b.PayrollId == id);
            return PartialView("_PayrollDeductionsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddPayrollDeduction()
        {
            ViewBag.employeesList = await _context.Employees.Where(b => b.IsActive).ToListAsync();
            ViewBag.payrollComponentTypesList = _lookup.GetLookupItems<PayrollComponentType>();
            return PartialView("_AddPayrollDeduction");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPayrollDeduction(PayrollDeduction item)
        {
            _context.PayrollDeductions.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("PayrollDeductionsList", new { id = item.PayrollId });
        }

        public async Task<IActionResult> DeletePayrollDeduction(long id)
        {
            var item = _context.PayrollDeductions.SingleOrDefault(b => b.Id == id);
            long prId = item.PayrollId;
            _context.PayrollDeductions.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("PayrollDeductionsList", new { id = prId });
        }
    }
}
