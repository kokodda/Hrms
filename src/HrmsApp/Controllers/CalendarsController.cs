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
    public class CalendarsController : Controller
    {
        private readonly HrmsDbContext _context;
        public CalendarsController(HrmsDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //calendars
        public async Task<IActionResult> CalendarsList()
        {
            var model = _context.Calendars.Where(b => b.IsActive).OrderBy(b => b.EffectiveFromDate);
            return PartialView("_CalendarsList", await model.ToListAsync());
        }

        public IActionResult AddCalendar()
        {
            return PartialView("_AddCalendar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCalendar(Calendar item)
        {
            _context.Calendars.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("CalendarsList");
        }

        public async Task<IActionResult> EditCalendar(int id)
        {
            var model = _context.Calendars.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditCalendar", await model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCalendar(Calendar item)
        {
            var model = await _context.Calendars.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("CalendarsList");
        }

        //holidays
        public async Task<IActionResult> HolidaysList()
        {
            var model = _context.Holidays.Where(b => b.IsActive).OrderBy(b => b.FromMonth).ThenBy(b => b.FromDay);
            return PartialView("_HolidaysList", await model.ToListAsync());
        }

        public IActionResult AddHoliday(long id)
        {
            return PartialView("_AddHoliday");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHoliday(Holiday item)
        {
            _context.Holidays.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("HolidaysList");
        }

        public async Task<IActionResult> EditHoliday(int id)
        {
            var model = _context.Holidays.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditHoliday", await model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHoliday(Holiday item)
        {
            var model = await _context.Holidays.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("HolidaysList");
        }

        //holiday variations
        public async Task<IActionResult> VariationsList(long? id)
        {
            //id is the calendar id (for filtering)
            var model = _context.HolidayVariations.Include(b => b.Calendar).Include(b => b.Holiday)
                                        .Where(b => (!id.HasValue || b.CalendarId == id) && b.FromDate.Year == DateTime.Now.Year && b.Calendar.IsActive && b.Holiday.IsActive && b.IsActive)
                                        .OrderBy(b => b.FromDate);
            var x = await _context.Calendars.SingleOrDefaultAsync(b => b.IsActive && ((!id.HasValue && b.IsDefault) || b.Id == id));
            if (x != null)
                ViewBag.calendarName = x.Name;
            else
                ViewBag.calendarName = "Calendar is Not Defined Yet.";

            return PartialView("_VariationsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddVariation(int id)
        {
            //id is the holiday id
            var calendarsList = await _context.Calendars.Where(b => b.IsActive).OrderBy(b => b.EffectiveFromDate).ToListAsync();
            if(calendarsList.Count() == 1)
            {
                ViewBag.calendarsList = null;
                ViewBag.calendarId = calendarsList.First().Id;
            }
            else
            {
                ViewBag.calendarsList = calendarsList;
                ViewBag.calendarId = null;
            }
            return PartialView("_AddVariation");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVariation(HolidayVariation item)
        {
            _context.HolidayVariations.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("VariationsList");
        }

        public async Task<IActionResult> EditVariation(long id)
        {
            var model = _context.HolidayVariations.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditVariation", await model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVariation(HolidayVariation item)
        {
            var model = await _context.HolidayVariations.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("VariationsList");
        }

        public async Task<IActionResult> ComponsateVariation(long id)
        {
            var x = await _context.HolidayVariations.SingleOrDefaultAsync(b => b.Id == id);
            var lst = await _context.EmployeeLeaveBalances.Include(b => b.Employee).Include(b => b.LeaveType)
                                .Where(b => b.Employee.IsActive && b.LeaveType.SysCode == "ANNUAL").ToListAsync();
            foreach(var item in lst)
            {
                item.BalanceDays += x.CompensateNbrDays;
            }
            x.IsComponsated = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("VariationsList");
        }

        //shifts
        public async Task<IActionResult> ShiftsList(int? id)
        {
            var model = _context.Shifts.Where(b => !id.HasValue || b.CalendarId == id);
            var x = await _context.Calendars.SingleOrDefaultAsync(b => b.IsActive && ((!id.HasValue && b.IsDefault) || b.Id == id));
            ViewBag.calendarId = x.Id;
            return PartialView("_ShiftsList", await model.ToListAsync());
        }

        public IActionResult AddShift()
        {
            return PartialView("_AddShift");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShift(Shift item)
        {
            await _context.Shifts.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShiftsList", new { id = item.CalendarId });
        }

        public async Task<IActionResult> EditShift(int id)
        {
            var model = await _context.Shifts.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditShift", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShift(Shift item)
        {
            var model = await _context.Shifts.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShiftsList", new { id = model.CalendarId });
        }

        public async Task<IActionResult> RemoveShift(int id)
        {
            var item = await _context.Shifts.SingleOrDefaultAsync(b => b.Id == id);
            long calendarId = item.CalendarId;
            _context.Shifts.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShiftsList", new { id = calendarId });
        }

        //rotations
        public async Task<IActionResult> RotationsList(int id, string shiftName)
        {
            var model = _context.ShiftRotations.Where(b => b.ShiftId == id).OrderBy(b => b.SortOrder);
            ViewBag.shiftId = id;
            ViewBag.shiftName = shiftName;
            return PartialView("_RotationsList", await model.ToListAsync());
        }

        public IActionResult AddRotation()
        {
            return PartialView("_AddRotation");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRotation(ShiftRotation item)
        {
            var s = await _context.Shifts.SingleOrDefaultAsync(b => b.Id == item.ShiftId);
            await _context.ShiftRotations.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("RotationsList", new { id = item.ShiftId, shiftName = s.Name });
        }

        public async Task<IActionResult> EditRotation(int id)
        {
            var model = await _context.ShiftRotations.Include(b => b.Shift).SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditRotation", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRotation(ShiftRotation item)
        {
            var model = await _context.ShiftRotations.Include(b => b.Shift).SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("RotationsList", new { id = model.ShiftId, shiftName = model.Shift.Name });
        }

        public async Task<IActionResult> RemoveRotation(int id)
        {
            var item = await _context.ShiftRotations.Include(b => b.Shift).SingleOrDefaultAsync(b => b.Id == id);
            long id1 = item.ShiftId;
            string sName = item.Shift.Name;
            _context.ShiftRotations.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("RotationsList", new { id = id1, shiftName = sName });
        }

        //companies
        public async Task<IActionResult> CompaniesList(long? id)
        {
            var model = _context.Companies.Include(b => b.OrgUnit);
            var defaultCalendar = await _context.Calendars.SingleOrDefaultAsync(b => b.IsDefault);
            if (!id.HasValue || id == defaultCalendar.Id)
                model.Where(b => !b.CalendarId.HasValue);
            else
                model.Where(b => b.CalendarId == id);
            return PartialView("_CompaniesList", await model.ToListAsync());
        }
    }
}