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
    public class OrganizationController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;

        public OrganizationController(HrmsDbContext context, ILookupServices lookup)
        {
            _context = context;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> OrgChart()
        {
            var model = _context.OrgUnits.Include(b => b.OrgUnitType);
            return PartialView("_OrgChart", await model.ToListAsync());
        }

        public IActionResult AddOrgUnit(long parentId, bool isSub = false)
        {
            ViewBag.parentName = _context.OrgUnits.SingleOrDefault(b => b.Id == parentId).Name;
            if (isSub)
            {
                ViewBag.orgUnitTypeId = _lookup.GetLookupItems<OrgUnitType>().SingleOrDefault(b => b.SysCode == "SUB").Id;
                return PartialView("_AddSubUnit");
            }
            else
            {
                ViewBag.orgUnitTypesList = _lookup.GetLookupItems<OrgUnitType>().Where(b => b.SysCode != "SUB");
                return PartialView("_AddOrgUnit");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrgUnit(OrgUnit item, long parentId)
        {
            //calculate ou code
            string parentCode = _context.OrgUnits.SingleOrDefault(b => b.Id == parentId).Code;
            int levelCodeLength = parentCode.Length + 3;
            int ouCode = _context.OrgUnits.Where(b => b.Code.StartsWith(parentCode) && b.Code.Length == levelCodeLength).Count() + 1;
            if (ouCode < 10)
                item.Code = parentCode + "." + "0" + ouCode.ToString();
            else
                item.Code = parentCode + "." + ouCode.ToString();
            item.CreatedDate = DateTime.Now.Date;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";

            _context.OrgUnits.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrgChart");
        }
    }
}