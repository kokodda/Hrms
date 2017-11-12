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
    public class ShiftsController : Controller
    {
        private readonly HrmsDbContext _context;
        public ShiftsController(HrmsDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCalendar(int id)
        {
            var model = await _context.Calendars.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_GetCalendar", model);
        }

        //shifts
        public async Task<IActionResult> ShiftsList()
        {
            var model = _context.Shifts.Include(b => b.Calendar);
            return PartialView("_ShiftsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddShift()
        {
            ViewBag.calendarsList = await _context.Calendars.Where(b => b.IsActive).OrderBy(b => b.EffectiveFromDate).ToListAsync();
            return PartialView("_AddShift");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShift(Shift item)
        {
            await _context.Shifts.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShiftsList");
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
            return RedirectToAction("ShiftsList");
        }

        public async Task<IActionResult> RemoveShift(int id)
        {
            var item = await _context.Shifts.SingleOrDefaultAsync(b => b.Id == id);
            _context.Shifts.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShiftsList");
        }

        //rotations
        public async Task<IActionResult> RotationsList(int id)
        {
            var model = _context.ShiftRotations.Where(b => b.ShiftId == id).OrderBy(b => b.SortOrder);
            ViewBag.shiftId = id;
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

        //groups
        public async Task<IActionResult> ShiftGroupsList(int id)
        {
            var model = _context.ShiftGroups.Include(b => b.GenericSubGroup).Where(b => b.ShiftId == id).OrderBy(b => b.SortOrder);
            ViewBag.shiftId = id;
            return PartialView("_ShiftGroupsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddShiftGroup()
        {
            ViewBag.subGroupsList = await _context.GenericSubGroups.Where(b => b.IsActive).ToListAsync();
            return PartialView("_AddShiftGroup");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShiftGroup(ShiftGroup item)
        {
            await _context.ShiftGroups.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShiftGroupsList", new { id = item.ShiftId });
        }

        public async Task<IActionResult> RemoveShiftGroup(int id)
        {
            var item = await _context.ShiftGroups.SingleOrDefaultAsync(b => b.Id == id);
            int shiftId = item.ShiftId;
            _context.ShiftGroups.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShiftGroupsList", new { id = shiftId });
        }
    }
}