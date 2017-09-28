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
    public class GroupsController : Controller
    {
        private readonly HrmsDbContext _context;

        public GroupsController(HrmsDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //generic groups
        public async Task<IActionResult> GroupsList()
        {
            var model = _context.GenericGroups.Where(b => b.IsActive).OrderBy(b => b.SortOrder);
            return PartialView("_GroupsList", await model.ToListAsync());
        }
        
        public IActionResult AddGroup()
        {
            return PartialView("_AddGroup");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGroup(GenericGroup item)
        {
            _context.GenericGroups.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("GroupsList");
        }

        //sub groups
        public async Task<IActionResult> SubGroupsList(int id)
        {
            var model = _context.GenericSubGroups.Where(b => b.GenericGroupId == id && b.IsActive).OrderBy(b => b.SortOrder);
            ViewBag.groupId = id;
            var x = await _context.GenericGroups.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.pTitle = x.Name;
            return PartialView("_SubGroupsList", await model.ToListAsync());
        }

        public IActionResult AddSubGroup()
        {
            return PartialView("_AddSubGroup");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubGroup(GenericSubGroup item)
        {
            _context.GenericSubGroups.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("SubGroupsList", new { id = item.GenericGroupId });
        }

        //members
        public async Task<IActionResult> MembersList(int id)
        {
            var model = _context.EmployeeGroups.Include(b => b.Employee).ThenInclude(b => b.Employments).ThenInclude(b => b.OrgUnit)
                                        .Where(b => b.GenericSubGroupId == id && b.Employee.Employments.FirstOrDefault(c => !c.IsActing && c.IsActive) != null);
            ViewBag.subGroupId = id;
            var x = await _context.GenericSubGroups.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.pTitle = x.Name;
            return PartialView("_MembersList", await model.ToListAsync());
        }

        public IActionResult AddMember()
        {
            ViewBag.employeesList = _context.Employees.Where(b => b.IsActive).Select(b => new { Id = b.Id, Name = (b.FirstName + " " + b.FatherName + " " + b.FamilyName) }).ToList();
            return PartialView("_AddMember");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember(EmployeeGroup item)
        {
            _context.EmployeeGroups.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("MembersList", new { id = item.GenericSubGroupId });
        }

        public async Task<IActionResult> RemoveMember(long id)
        {
            var item = await _context.EmployeeGroups.SingleOrDefaultAsync(b => b.Id == id);
            int subGroupId = item.GenericSubGroupId;
            _context.EmployeeGroups.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("MembersList", new { id = subGroupId });
        }
    }
}