using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HrmsModel.Models;
using HrmsModel.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace HrmsApp.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;
        private readonly IHostingEnvironment _environment;

        public CompaniesController(HrmsDbContext context, ILookupServices lookup, IHostingEnvironment environment)
        {
            _context = context;
            _lookup = lookup;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //companies
        public async Task<IActionResult> CompaniesList(long? id)
        {
            var model = _context.Companies.Include(b => b.OrgUnit).Include(b => b.CompanyGroupMembers).Include(b => b.Calendar)
                                .Where(b => !id.HasValue || b.CompanyGroupMembers.Any(c => c.CompanyGroupId == id));
            return PartialView("_CompaniesList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddCompany()
        {
            ViewBag.calendarsList = await _context.Calendars.Where(b => b.IsActive).ToListAsync();
            return PartialView("_AddCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompany(OrgUnit item, string ShortName, string OthShortName, string LegalTypeCode, long? CalendarId)
        {
            int orgUnitTypeId = _lookup.GetLookupItems<OrgUnitType>().SingleOrDefault(b => b.SysCode == "COMPANY").Id;
            int cnt = await _context.OrgUnits.Where(b => b.OrgUnitTypeId == orgUnitTypeId).CountAsync() + 1;
            if (cnt > 9)
                item.Code = cnt.ToString();
            else
                item.Code = "0" + cnt.ToString();
            item.OrgUnitTypeId = orgUnitTypeId;
            item.SortOrder = 1;
            item.IsVacant = true;
            item.IsActive = true;
            item.CreatedDate = DateTime.Now.Date;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            await _context.OrgUnits.AddAsync(item);

            //add company entity
            Company comp = new Company();
            comp.Id = item.Id;
            comp.LegalTypeCode = LegalTypeCode;
            comp.ShortName = ShortName;
            comp.OthShortName = OthShortName;
            comp.CalendarId = CalendarId;
            await _context.Companies.AddAsync(comp);

            await _context.SaveChangesAsync();
            return RedirectToAction("CompaniesList");
        }

        public async Task<IActionResult> EditCompany(long id)
        {
            var model = await _context.Companies.Include(b => b.OrgUnit).SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.calendarsList = await _context.Calendars.Where(b => b.IsActive).ToListAsync();
            return PartialView("_EditCompany", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCompany(Company item)
        {
            var model = await _context.Companies.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("CompaniesList");
        }

        public async Task<IActionResult> RemoveFromGroup(long id, long? groupId)
        {
            var model = await _context.CompanyGroupMembers.FirstOrDefaultAsync(b => b.CompanyId == id && (!groupId.HasValue || b.CompanyGroupId == groupId));
            _context.CompanyGroupMembers.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("CompaniesList");
        }

        //company groups
        public async Task<IActionResult> GroupsList()
        {
            var model = _context.CompanyGroups.Include(b => b.OrgUnit).Include(b => b.CompanyGroupMembers);
            return PartialView("_GroupsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddGroup()
        {
            int orgUnitTypeId = _lookup.GetLookupItems<OrgUnitType>().SingleOrDefault(b => b.SysCode == "COMPANY").Id;
            ViewBag.companiesList = await _context.OrgUnits.Where(b => b.OrgUnitTypeId == orgUnitTypeId).ToListAsync();
            return PartialView("_AddGroup");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGroup(CompanyGroup item)
        {
            if (item.MotherOrgUnitId.HasValue)
            {
                item.Name = null;
                item.OthName = null;
            }
            await _context.CompanyGroups.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("GroupsList");
        }

        public async Task<IActionResult> EditGroup(long id)
        {
            var model = await _context.CompanyGroups.Include(b => b.OrgUnit).SingleOrDefaultAsync(b => b.Id == id);
            int orgUnitTypeId = _lookup.GetLookupItems<OrgUnitType>().SingleOrDefault(b => b.SysCode == "COMPANY").Id;
            ViewBag.companiesList = await _context.OrgUnits.Where(b => b.OrgUnitTypeId == orgUnitTypeId).ToListAsync();
            return PartialView("_EditGroup", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(CompanyGroup item)
        {
            var model = await _context.CompanyGroups.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            if(item.MotherOrgUnitId.HasValue)
            {
                model.Name = null;
                model.OthName = null;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("GroupsList");
        }

        public async Task<IActionResult> AddGroupMember(long id)
        {
            int orgUnitTypeId = _lookup.GetLookupItems<OrgUnitType>().SingleOrDefault(b => b.SysCode == "COMPANY").Id;
            List<long> Ids = new List<long>(); //ids of companies that already are member of the group
            Ids.AddRange(await _context.CompanyGroupMembers.Where(b => b.CompanyGroupId == id && b.IsActive).Select(b => b.CompanyId).ToListAsync());
            ViewBag.companiesList = await _context.OrgUnits.Where(b => b.OrgUnitTypeId == orgUnitTypeId && !Ids.Contains(b.Id)).ToListAsync();

            return PartialView("_AddGroupMember");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGroupMember(CompanyGroupMember item)
        {
            await _context.CompanyGroupMembers.AddAsync(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("CompanyGroupsList");
        }




        //uploads
        [HttpPost]
        public async Task<IActionResult> photoUpload(ICollection<IFormFile> files, long id, string owner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var file = files.FirstOrDefault();
                    string photoName = file.FileName;
                    byte[] photoFile;
                    using (MemoryStream str = new MemoryStream())
                    {
                        await file.OpenReadStream().CopyToAsync(str);
                        photoFile = str.ToArray();
                    }
                    if(owner == "GROUP")
                    {
                        var grp = await _context.CompanyGroups.SingleOrDefaultAsync(b => b.Id == id);
                        grp.Logo = photoFile;
                    }
                    else
                    {
                        var comp = await _context.Companies.SingleOrDefaultAsync(b => b.Id == id);
                        comp.Logo = photoFile;
                    }
                    await _context.SaveChangesAsync();
                    return Json("Successfull");
                }
                catch
                {
                    return Json("Failed to Upload Photo! Please try again.");
                }
            }
            return Json("Failed to Upload Photo! Please try again.");
        }
    }
}