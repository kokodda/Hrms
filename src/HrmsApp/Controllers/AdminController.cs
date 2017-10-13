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

        public IActionResult DictionaryList(string typeName)
        {
            ViewBag.typeName = typeName;
            List<ILookup> model = new List<ILookup>();
            switch (typeName)
            {
                case "OrgUnitType":
                    model = _context.OrgUnitTypes.ToList<ILookup>();
                    break;
                case "EmploymentType":
                    model = _context.EmploymentTypes.ToList<ILookup>();
                    break;
                case "AllowanceType":
                    model = _context.AllowanceTypes.ToList<ILookup>();
                    break;
                case "CompetencyAreaType":
                    model = _context.CompetencyAreaTypes.ToList<ILookup>();
                    break;
                case "LeaveType":
                    model = _context.LeaveTypes.ToList<ILookup>();
                    break;
                case "SalaryScaleType":
                    model = _context.SalaryScaleTypes.ToList<ILookup>();
                    break;
                case "StandardTitleType":
                    model = _context.StandardTitleTypes.ToList<ILookup>();
                    break;
                case "DocumentType":
                    model = _context.DocumentTypes.ToList<ILookup>();
                    break;
                case "FamilyMemberType":
                    model = _context.FamilyMemberTypes.ToList<ILookup>();
                    break;
                case "QualificationType":
                    model = _context.QualificationTypes.ToList<ILookup>();
                    break;
                case "LanguageType":
                    model = _context.LanguageTypes.ToList<ILookup>();
                    break;
                case "PromotionType":
                    model = _context.PromotionTypes.ToList<ILookup>();
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
            return PartialView("_DictionaryList", model);
        }

        public IActionResult EditDictionary(string typeName, int id, bool status)
        {
            switch (typeName)
            {
                case "OrgUnitType":
                    _context.OrgUnitTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<OrgUnitType>();
                    break;
                case "EmploymentType":
                    _context.EmploymentTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<EmploymentType>();
                    break;
                case "AllowanceType":
                    _context.AllowanceTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<AllowanceType>();
                    break;
                case "CompetencyAreaType":
                    _context.CompetencyAreaTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<CompetencyAreaType>();
                    break;
                case "LeaveType":
                    _context.LeaveTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<LeaveType>();
                    break;
                case "SalaryScaleType":
                    _context.SalaryScaleTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<SalaryScaleType>();
                    break;
                case "StandardTitleType":
                    _context.StandardTitleTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<StandardTitleType>();
                    break;
                case "DocumentType":
                    _context.DocumentTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<DocumentType>();
                    break;
                case "FamilyMemberType":
                    _context.FamilyMemberTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<FamilyMemberType>();
                    break;
                case "QualificationType":
                    _context.QualificationTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<QualificationType>();
                    break;
                case "LanguageType":
                    _context.LanguageTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<LanguageType>();
                    break;
                case "PromotionType":
                    _context.PromotionTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<PromotionType>();
                    break;
                case "Governorate":
                    _context.Governorates.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    break;
                case "Nationality":
                    _context.Nationalities.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    break;
            }
            return RedirectToAction("DictionaryList", new { typeName = typeName });
        }


    }
}