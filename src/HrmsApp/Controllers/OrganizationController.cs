using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HrmsModel.Models;
using HrmsModel.Data;
using Microsoft.EntityFrameworkCore;
using HrmsApp.Models;

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
            List<OrgUnitViewModel> model = new List<OrgUnitViewModel>();
            //check if new setup
            if (_context.OrgUnits.Count() == 0)
                return PartialView("_OrgChart", model);

            string rootCode;
            if (id.HasValue)
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

            var x = _context.OrgUnits.Include(b => b.OrgUnitType)
                                .Where(b => !id.HasValue || (b.Code.StartsWith(rootCode) && b.Code.Length <= maxLevelLength))
                                .OrderBy(b => b.Code.Length).ThenBy(b => b.SortOrder);
            foreach (var x1 in x)
            {
                OrgUnitViewModel item = new OrgUnitViewModel();
                item.OrgUnit = x1;
                //parent
                if (x1.Code.Length == 2)
                    item.ParentName = null;
                else
                    item.ParentName = _context.OrgUnits.SingleOrDefault(b => b.Code == x1.Code.Substring(0, x1.Code.Length - 3)).Name;
                //headed by
                var pos = await _context.Employments.Include(b => b.Employee).SingleOrDefaultAsync(b => b.OrgUnitId == x1.Id && b.IsActive && b.IsHead);
                if (pos != null)
                    item.HeadedByName = pos.Employee.FirstName + " " + pos.Employee.FamilyName;
                else
                    item.HeadedByName = "VACANT";
                //totals
                item.TotalPositions = await _context.Positions.Where(b => b.OrgUnitId == id).CountAsync() + 1;
                item.TotalPersonnel = await _context.Employments.Where(b => b.OrgUnitId == id && b.IsActive).CountAsync();
                item.TotalVacancy = await _context.Positions.Where(b => b.OrgUnitId == id).SumAsync(b => b.TotalVacant);
                model.Add(item);
            }
            return PartialView("_OrgChart", model);
        }

        public IActionResult AddOrgUnit(long? parentId, bool isSub = false)
        {
            ViewBag.orgUnitTypeId = null;
            if (isSub)
                ViewBag.orgUnitTypeId = _lookup.GetLookupItems<OrgUnitType>().SingleOrDefault(b => b.SysCode == "SUB").Id;
            else
            {
                if (parentId.HasValue)
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
                int seq = _context.OrgUnits.Where(b => b.Code.StartsWith(parentCode) && b.Code.Length == parentCode.Length + 3).Count() + 1;
                if (seq < 10)
                    item.Code = parentCode + "." + "0" + seq.ToString();
                else
                    item.Code = parentCode + "." + seq.ToString();
            }

            item.IsVacant = true;
            item.CreatedDate = DateTime.Now.Date;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";

            _context.OrgUnits.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrgChart", new { id = parentId });
        }

        public async Task<IActionResult> EditOrgUnit(long id)
        {
            var model = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == id);
            var parent = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Code == model.Code.Substring(0, model.Code.Length - 3));
            if (parent != null)
                ViewBag.parentId = parent.Id;
            else
                ViewBag.parentId = null;
            ViewBag.orgUnitsList = await _context.OrgUnits.Where(b => b.Id != id && b.IsActive).ToListAsync();
            ViewBag.orgUnitTypesList = _lookup.GetLookupItems<OrgUnitType>();
            return PartialView("_EditOrgUnit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrgUnit(OrgUnit item, long parentId)
        {
            var model = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == item.Id);
            string oldCode = model.Code;
            var newParent = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == parentId);
            await TryUpdateModelAsync(model);

            if (!model.Code.StartsWith(newParent.Code)) //in case parent business unit has changed, then generate new code
            {
                string oldParentCode = model.Code.Substring(0, model.Code.Length - 3);
                int seq = _context.OrgUnits.Where(b => b.Code.StartsWith(newParent.Code) && b.Code.Length == newParent.Code.Length + 3).Count() + 1;
                if (seq < 10)
                    model.Code = newParent.Code + "." + "0" + seq.ToString();
                else
                    model.Code = newParent.Code + "." + seq.ToString();

                //regenerate all old parent children codes
                var children = await _context.OrgUnits.Where(b => b.Code != oldCode && b.Code.StartsWith(oldParentCode) && b.Code.Length == oldParentCode.Length + 3)
                                            .OrderBy(b => b.SortOrder).ToListAsync();
                seq = 0;
                foreach(var ou in children)
                {
                    seq++;
                    if (seq < 10)
                        ou.Code = oldParentCode + "." + "0" + seq.ToString();
                    else
                        ou.Code = oldParentCode + "." + seq.ToString();
                }
            }
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";

            await _context.SaveChangesAsync();
            return RedirectToAction("OrgChart", new { id = parentId });
        }
        
    }
}