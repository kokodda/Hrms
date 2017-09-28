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

        public async Task<IActionResult> OrgChart(long? id, bool isUp = false)
        {
            //check if new setup
            if(_context.OrgUnits.Count() == 0)
                return PartialView("_OrgChart", await _context.OrgUnits.ToListAsync());

            string rootCode;
            if(id.HasValue)
            {
                long rootId = id.Value;
                if (isUp)
                {
                    var ou = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == id);
                    rootCode = ou.Code.Substring(0, ou.Code.Length - 3);
                }
                else
                {
                    var ou = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == rootId);
                    rootCode = ou.Code;
                }
            }
            else
            {
                var ou = await _context.OrgUnits.FirstOrDefaultAsync(b => b.Code.Length == 2);
                rootCode = ou.Code;
            }
            //get down to the third level under the selected node
            int maxLevelLength = rootCode.Length + 6;

            var model = _context.OrgUnits.Include(b => b.OrgUnitType)
                                .Where(b => !id.HasValue || (b.Code.StartsWith(rootCode) && b.Code.Length <= maxLevelLength))
                                .OrderBy(b => b.Code.Length).ThenBy(b => b.SortOrder);
            return PartialView("_OrgChart", await model.ToListAsync());
        }

        public IActionResult AddOrgUnit(long? parentId, bool isSub = false)
        {
            ViewBag.orgUnitTypeId = null;
            if (isSub)
                ViewBag.orgUnitTypeId = _lookup.GetLookupItems<OrgUnitType>().SingleOrDefault(b => b.SysCode == "SUB").Id;
            else
            {
                if(parentId.HasValue)
                {
                    int sortOrder = _context.OrgUnits.Include(b => b.OrgUnitType).SingleOrDefault(b => b.Id == parentId).OrgUnitType.SortOrder;
                    ViewBag.orgUnitTypesList = _lookup.GetLookupItems<OrgUnitType>().Where(b => b.SysCode != "SUB" && b.SortOrder > sortOrder);
                }
                else
                    ViewBag.orgUnitTypeId = _lookup.GetLookupItems<OrgUnitType>().SingleOrDefault(b => b.SysCode == "COMPANY").Id;
            }
            return PartialView("_AddOrgUnit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrgUnit(OrgUnit item, long? parentId)
        {
            //calculate ou code
            if (!parentId.HasValue) //new company
                item.Code = "01";
            else
            {
                string parentCode = _context.OrgUnits.SingleOrDefault(b => b.Id == parentId).Code;
                int levelCodeLength = parentCode.Length + 3;
                int ouCode = _context.OrgUnits.Where(b => b.Code.StartsWith(parentCode) && b.Code.Length == levelCodeLength).Count() + 1;
                if (ouCode < 10)
                    item.Code = parentCode + "." + "0" + ouCode.ToString();
                else
                    item.Code = parentCode + "." + ouCode.ToString();
            }
            
            item.CreatedDate = DateTime.Now.Date;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";

            _context.OrgUnits.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrgChart", new { id = parentId});
        }
    }
}