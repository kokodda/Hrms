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
    public class CompaniesController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;

        public CompaniesController(HrmsDbContext context, ILookupServices lookup)
        {
            _context = context;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            return View();
        }

        //companies
        public async Task<IActionResult> CompaniesList()
        {
            var model = _context.Companies.Include(b => b.OrgUnit);
            return PartialView("_CompaniesList", await model.ToListAsync());
        }

        public IActionResult AddCompany()
        {
            return PartialView("_AddCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompany(OrgUnit item, string ShortName, string OthShortName, string LegalTypeCode)
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
            await _context.Companies.AddAsync(comp);

            await _context.SaveChangesAsync();
            return RedirectToAction("CompaniesList");
        }

        //company groups
        public async Task<IActionResult> CompanyGroupsList()
        {
            var model = _context.CompanyGroups.Include(b => b.OrgUnit).Include(b => b.CompanyGroupMembers);
            return PartialView("_CompanyGroupsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddCompanyGroup()
        {
            int orgUnitTypeId = _lookup.GetLookupItems<OrgUnitType>().SingleOrDefault(b => b.SysCode == "COMPANY").Id;
            ViewBag.companiesList = await _context.OrgUnits.Where(b => b.OrgUnitTypeId == orgUnitTypeId).ToListAsync();
            return PartialView("_AddCompanyGroup");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompanyGroup(CompanyGroup item)
        {
            await _context.CompanyGroups.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("CompanyGroupsList");
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
    }
}