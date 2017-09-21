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
    public class AdminController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;

        public AdminController(HrmsDbContext context, ILookupServices lookup)
        {
            _context = context;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TypesList(string typeName)
        {
            ViewBag.typeName = typeName;
            List<ILookup> model = new List<ILookup>();
            switch (typeName)
            {
                case "OrgUnitType":
                    model = _lookup.GetLookupItems<OrgUnitType>().ToList();
                    break;
                case "EmployeeType":
                    model = _lookup.GetLookupItems<EmployeeType>().ToList();
                    break;
                case "AllowanceType":
                    model = _lookup.GetLookupItems<AllowanceType>().ToList();
                    break;
                case "CompetencyAreaType":
                    model = _lookup.GetLookupItems<CompetencyAreaType>().ToList();
                    break;
                case "LeaveType":
                    model = _lookup.GetLookupItems<LeaveType>().ToList();
                    break;
                case "SalaryScaleType":
                    model = _lookup.GetLookupItems<SalaryScaleType>().ToList();
                    break;
                case "StandardTitleType":
                    model = _lookup.GetLookupItems<StandardTitleType>().ToList();
                    break;
                case "DocumentType":
                    model = _lookup.GetLookupItems<DocumentType>().ToList();
                    break;
                case "FamilyMemberType":
                    model = _lookup.GetLookupItems<FamilyMemberType>().ToList();
                    break;
                case "QualificationType":
                    model = _lookup.GetLookupItems<QualificationType>().ToList();
                    break;
                case "LanguageType":
                    model = _lookup.GetLookupItems<LanguageType>().ToList();
                    break;
                case "Governorate":
                    var x1 = _context.Governorates.ToList();
                    foreach (var x in x1)
                        model.Add(new LookupItem { Id = x.Id, Name = x.Name, OthName = x.OthName, SysCode = "", SortOrder = x.SortOrder, IsActive = x.IsActive });
                    break;
                case "Nationality":
                    var x2 = _context.Nationalities.ToList();
                    foreach (var x in x2)
                        model.Add(new LookupItem { Id = x.Id, Name = x.Name, OthName = x.OthName, SysCode = "", SortOrder = x.SortOrder, IsActive = x.IsActive });
                    break;
            }
            return PartialView("_TypesList", model);
        }


        public async Task<IActionResult> CompetencyAreasList()
        {
            var model = _context.CompetencyAreaTypes.OrderBy(b => b.SortOrder);
            return PartialView("_CompetencyAreaTypesList", await model.ToListAsync());
        }
    }
}