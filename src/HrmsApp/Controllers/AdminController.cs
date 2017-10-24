using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HrmsModel.Models;
using HrmsModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HrmsApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;
        private readonly IHostingEnvironment _environment;

        public AdminController(HrmsDbContext context, ILookupServices lookup, IHostingEnvironment environment)
        {
            _context = context;
            _lookup = lookup;
            _environment = environment;
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
                case "PayrollComponentType":
                    model = _context.PayrollComponentTypes.ToList<ILookup>();
                    break;
                case "AttendanceActionType":
                    model = _context.AttendanceActionTypes.ToList<ILookup>();
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
                case "PayrollComponentType":
                    _context.PayrollComponentTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<PayrollComponentType>();
                    break;
                case "AttendanceActionType":
                    _context.AttendanceActionTypes.SingleOrDefault(b => b.Id == id).IsActive = status;
                    _context.SaveChanges();
                    _lookup.Refresh<AttendanceActionType>();
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

        //carousel
        public async Task<IActionResult> CarouselItemsList()
        {
            var model = _context.CarouselItems;
            return PartialView("_CarouselItemsList", await model.ToListAsync());
        }

        public IActionResult AddCarouselItem()
        {
            return PartialView("_AddCarouselItem");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCarouselItem(CarouselItem item)
        {
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.CarouselItems.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("CarouselItemsList");
        }

        public async Task<IActionResult> EditCarouselItem(long id)
        {
            var model = await _context.CarouselItems.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditCarouselItem", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCarouselItem(CarouselItem item)
        {
            var model = await _context.CarouselItems.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";
            await _context.SaveChangesAsync();
            return RedirectToAction("CarouselItemsList");
        }

        //site contents
        public async Task<IActionResult> SiteContentsList()
        {
            var model = _context.SiteContents.Where(b => b.IsActive);
            return PartialView("_SiteContentsList", await model.ToListAsync());
        }

        public IActionResult AddSiteContent()
        {
            return PartialView("_AddSiteContent");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSiteContent(SiteContent item)
        {
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            await _context.SiteContents.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("SiteContentsList");
        }

        public async Task<IActionResult> EditSiteContent(long id)
        {
            var model = await _context.SiteContents.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditSiteContent", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSiteContent(SiteContent item)
        {
            var model = await _context.SiteContents.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";
            await _context.SaveChangesAsync();
            return RedirectToAction("SiteContentsList");
        }

        //uploads
        [HttpPost]
        public async Task<IActionResult> fileUpload(ICollection<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var file = files.FirstOrDefault();
                    if (file.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_environment.ContentRootPath, @"wwwroot\carousels");
                        DirectoryInfo di = new DirectoryInfo(Path.GetFullPath(uploadsFolder));
                        using (var fileStream = new FileStream(Path.Combine(uploadsFolder, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    return Json("Successfull");
                }
                catch (Exception e)
                {
                    return Json("error! Failed to Upload Files. Please re-try later." + e.Message);
                }
            }
            return Json("error! Input File(s) are corrupted. Please re-enter files.");
        }
    }
}