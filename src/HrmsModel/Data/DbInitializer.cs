using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrmsModel.Models;

namespace HrmsModel.Data
{
    public class DbInitializer
    {
        public static void Initialize(HrmsDbContext _context)
        {
            //_context.Database.EnsureCreated();

            // Look for any seeded type.
            if (!_context.OrgUnitTypes.Any())
            {
                var orgUnitTypes = new OrgUnitType[]
                {
                    new OrgUnitType { Name = "Company", OthName = "Company", SysCode = "COMPANY", SortOrder = 10, IsActive = true },
                    new OrgUnitType { Name = "Division", OthName = "Division", SysCode = "DIVISION", SortOrder = 20, IsActive = true },
                    new OrgUnitType { Name = "Department", OthName = "Department", SysCode = "DEPARTMENT", SortOrder = 30, IsActive = true },
                    new OrgUnitType { Name = "Section", OthName = "Section", SysCode = "SECTION", SortOrder = 40, IsActive = true },
                    new OrgUnitType { Name = "Unit", OthName = "Unit", SysCode = "UNIT", SortOrder = 50, IsActive = true },
                    new OrgUnitType { Name = "Sub", OthName = "Sub", SysCode = "SUB", SortOrder = 60, IsActive = true }
                };
                foreach (OrgUnitType x in orgUnitTypes)
                    _context.OrgUnitTypes.Add(x);
                _context.SaveChanges();
            }

            if(!_context.GradeGroups.Any())
            {
                var gradeGroups = new GradeGroup[]
                {
                    new GradeGroup { Code = "G1", Name = "G1", OthName = "G1", SortOrder = 1, IsActive = true },
                    new GradeGroup { Code = "G2", Name = "G2", OthName = "G2", SortOrder = 2, IsActive = true },
                    new GradeGroup { Code = "G3", Name = "G3", OthName = "G3", SortOrder = 3, IsActive = true },
                    new GradeGroup { Code = "G4", Name = "G4", OthName = "G4", SortOrder = 4, IsActive = true },
                    new GradeGroup { Code = "G5", Name = "G5", OthName = "G5", SortOrder = 5, IsActive = true }
                };
                foreach (GradeGroup x in gradeGroups)
                    _context.GradeGroups.Add(x);
                _context.SaveChanges();
            }

            if(!_context.SalarySteps.Any())
            {
                for(int i = 0; i <= 10; i++)
                {
                    string s = "s" + i.ToString();
                    _context.SalarySteps.Add(new SalaryStep { Code = s, Name = s, OthName = s, SortOrder = i + 1, SalaryIncrPctg = 100 + i * 5 });
                }
                _context.SaveChanges();
            }
            
            if(!_context.JobGrades.Any())
            {
                var jobGrades = new JobGrade[]
                {
                    new JobGrade { Code = "U", Name = "U", OthName = "U", GradeGroupId = 1, SalaryIncrPctg = 0, SortOrder = 1, IsActive = true },
                    new JobGrade { Code = "A", Name = "A", OthName = "A", GradeGroupId = 1, SalaryIncrPctg = 25, SortOrder = 2, IsActive = true },
                    new JobGrade { Code = "B", Name = "B", OthName = "B", GradeGroupId = 1, SalaryIncrPctg = 35, SortOrder = 3, IsActive = true },
                    new JobGrade { Code = "C", Name = "C", OthName = "C", GradeGroupId = 1, SalaryIncrPctg = 50, SortOrder = 4, IsActive = true },
                    new JobGrade { Code = "1", Name = "1", OthName = "1", GradeGroupId = 2, SalaryIncrPctg = 15, SortOrder = 5, IsActive = true },
                    new JobGrade { Code = "2", Name = "2", OthName = "2", GradeGroupId = 2, SalaryIncrPctg = 15, SortOrder = 6, IsActive = true },
                    new JobGrade { Code = "3", Name = "3", OthName = "3", GradeGroupId = 3, SalaryIncrPctg = 0, SortOrder = 7, IsActive = true }
                };
                foreach (JobGrade x in jobGrades)
                    _context.JobGrades.Add(x);
                _context.SaveChanges();
            }

            if (!_context.LeaveTypes.Any())
            {
                var leaveTypes = new LeaveType[]
                {
                    new LeaveType { Name = "Annual", OthName = "Annual", SysCode = "ANNUAL", SortOrder = 10, IsActive = true },
                    new LeaveType { Name = "Sick", OthName = "Sick", SysCode = "SICK", SortOrder = 20, IsActive = true },
                    new LeaveType { Name = "Business Travel", OthName = "Business Travel", SysCode = "BUSINESS", SortOrder = 30, IsActive = true },
                    new LeaveType { Name = "Half Day", OthName = "Half Day", SysCode = "HALF_DAY", SortOrder = 40, IsActive = true },
                    new LeaveType { Name = "Unpaid", OthName = "Unpaid", SysCode = "UNPAID", SortOrder = 50, IsActive = true },
                    new LeaveType { Name = "Maternity", OthName = "Maternit", SysCode = "MATERNITY", SortOrder = 60, IsActive = true },
                    new LeaveType { Name = "Study", OthName = "Study", SysCode = "STUDY", SortOrder = 70, IsActive = true }
                };
                foreach (LeaveType x in leaveTypes)
                    _context.LeaveTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.AllowanceTypes.Any())
            {
                var allowanceTypes = new AllowanceType[]
                {
                    new AllowanceType { Name = "Job Nature", OthName = "Job Nature", SysCode = "JOB_NATURE", SortOrder = 10, IsActive = true },
                    new AllowanceType { Name = "Speciality", OthName = "Speciality", SysCode = "SPECIALITY", SortOrder = 20, IsActive = true },
                    new AllowanceType { Name = "House", OthName = "House", SysCode = "HOUSE", SortOrder = 30, IsActive = true },
                    new AllowanceType { Name = "Transportation", OthName = "Transportation", SysCode = "TRANSPORTATION", SortOrder = 40, IsActive = true },
                    new AllowanceType { Name = "Mobile Bill", OthName = "Mobile Bill", SysCode = "MOBILE_BILL", SortOrder = 50, IsActive = true }
                };
                foreach (AllowanceType x in allowanceTypes)
                    _context.AllowanceTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.StandardTitleTypes.Any())
            {
                var standardTitleTypes = new StandardTitleType[]
                {
                    new StandardTitleType { Name = "General Manager", OthName = "General Manager", SysCode = "GM", SortOrder = 10, IsActive = true },
                    new StandardTitleType { Name = "Director", OthName = "Director", SysCode = "DIRECTOR", SortOrder = 20, IsActive = true },
                    new StandardTitleType { Name = "Senior Manager", OthName = "Senior Manager", SysCode = "SRMGR", SortOrder = 30, IsActive = true },
                    new StandardTitleType { Name = "Manager", OthName = "Manager", SysCode = "MGR", SortOrder = 40, IsActive = true },
                    new StandardTitleType { Name = "Assistant Manager", OthName = "Assistant Manager", SysCode = "AMGR", SortOrder = 50, IsActive = true },
                    new StandardTitleType { Name = "Supervisor", OthName = "Supervisor", SysCode = "SUPERVISOR", SortOrder = 60, IsActive = true },
                    new StandardTitleType { Name = "Coordinator", OthName = "Coordinator", SysCode = "COORDINATOR", SortOrder = 70, IsActive = true },
                    new StandardTitleType { Name = "Clerck", OthName = "Clerck", SysCode = "CLERCK", SortOrder = 80, IsActive = true },
                    new StandardTitleType { Name = "Labor", OthName = "Labor", SysCode = "LABOR", SortOrder = 90, IsActive = true }
                };
                foreach (StandardTitleType x in standardTitleTypes)
                    _context.StandardTitleTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.CompetencyAreaTypes.Any())
            {
                var competencyAreaTypes = new CompetencyAreaType[]
                {
                    new CompetencyAreaType { Name = "Domain Technical Knowledge", OthName = "Domain Technical Knowledge", SysCode = "DOMAIN", SortOrder = 10, IsActive = true },
                    new CompetencyAreaType { Name = "Management", OthName = "Management", SysCode = "MGMT", SortOrder = 20, IsActive = true },
                    new CompetencyAreaType { Name = "Soft Skills", OthName = "Soft Skills", SysCode = "SOFT_SKILLS", SortOrder = 30, IsActive = true },
                    new CompetencyAreaType { Name = "Quality", OthName = "Quality", SysCode = "QUALITY", SortOrder = 40, IsActive = true },
                    new CompetencyAreaType { Name = "Safety", OthName = "Safety", SysCode = "SAFETY", SortOrder = 50, IsActive = true },
                    new CompetencyAreaType { Name = "ICDL", OthName = "ICDL", SysCode = "ICDL", SortOrder = 60, IsActive = true },
                    new CompetencyAreaType { Name = "English", OthName = "English", SysCode = "ENGLISH", SortOrder = 70, IsActive = true }
                };
                foreach (CompetencyAreaType x in competencyAreaTypes)
                    _context.CompetencyAreaTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.SalaryScaleTypes.Any())
            {
                var salaryScaleTypes = new SalaryScaleType[]
                {
                    new SalaryScaleType { Name = "Compatriot", OthName = "Compatriot", SysCode = "COMPAT", SortOrder = 10, IsActive = true },
                    new SalaryScaleType { Name = "Repatriate", OthName = "Repatriate", SysCode = "REPAT", SortOrder = 20, IsActive = true },
                    new SalaryScaleType { Name = "Expatriate", OthName = "Expatriate", SysCode = "EXPAT", SortOrder = 30, IsActive = true }
                };
                foreach (SalaryScaleType x in salaryScaleTypes)
                    _context.SalaryScaleTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.CompetencyCategories.Any())
            {
                var compCats = new CompetencyCategory[]
                {
                    new CompetencyCategory { Name = "Business Focus", OthName = "", SortOrder = 10, IsActive = true },
                    new CompetencyCategory { Name = "Accountability", OthName = "", SortOrder = 20, IsActive = true },
                    new CompetencyCategory { Name = "Drive & Resillience", OthName = "", SortOrder = 30, IsActive = true },
                    new CompetencyCategory { Name = "Problem Solving", OthName = "", SortOrder = 40, IsActive = true },
                    new CompetencyCategory { Name = "Developing People", OthName = "", SortOrder = 50, IsActive = true },
                    new CompetencyCategory { Name = "Customer Orientation", OthName = "", SortOrder = 60, IsActive = true },
                    new CompetencyCategory { Name = "Execution Excellence", OthName = "", SortOrder = 70, IsActive = true },
                    new CompetencyCategory { Name = "Academic", OthName = "", SortOrder = 1, IsActive = true }
                };
                foreach (CompetencyCategory x in compCats)
                    _context.CompetencyCategories.Add(x);
                _context.SaveChanges();
            }

            if (!_context.CompetencySubCategories.Any())
            {
                var compSubCats = new CompetencySubCategory[]
                {
                    new CompetencySubCategory { Name = "Financial and Profitability Orientation", OthName = "", CompetencyCategoryId = 1, Description = "", SortOrder = 10, IsActive = true },
                    new CompetencySubCategory { Name = "Enterpreneurial Skills", OthName = "", CompetencyCategoryId = 1, Description = "", SortOrder = 20, IsActive = true },
                    new CompetencySubCategory { Name = "Strategic Thinking", OthName = "", CompetencyCategoryId = 1, Description = "", SortOrder = 30, IsActive = true },
                    new CompetencySubCategory { Name = "Planning and Budgeting", OthName = "", CompetencyCategoryId = 1, Description = "", SortOrder = 40, IsActive = true },
                    new CompetencySubCategory { Name = "Organization and Resource Management", OthName = "", CompetencyCategoryId = 1, Description = "", SortOrder = 50, IsActive = true },
                    new CompetencySubCategory { Name = "Involvement and Ownership", OthName = "", CompetencyCategoryId = 2, Description = "", SortOrder = 60, IsActive = true },
                    new CompetencySubCategory { Name = "Responsibility", OthName = "", CompetencyCategoryId = 2, Description = "", SortOrder = 70, IsActive = true },
                    new CompetencySubCategory { Name = "Discipline", OthName = "", CompetencyCategoryId = 2, Description = "", SortOrder = 80, IsActive = true },
                    new CompetencySubCategory { Name = "Organizational Contribution", OthName = "", CompetencyCategoryId = 2, Description = "", SortOrder = 90, IsActive = true },
                    new CompetencySubCategory { Name = "Performance Orientation", OthName = "", CompetencyCategoryId = 2, Description = "", SortOrder = 100, IsActive = true },
                    new CompetencySubCategory { Name = "Team Work", OthName = "", CompetencyCategoryId = 2, Description = "", SortOrder = 110, IsActive = true },
                    new CompetencySubCategory { Name = "Initiative", OthName = "", CompetencyCategoryId = 3, Description = "", SortOrder = 120, IsActive = true },
                    new CompetencySubCategory { Name = "Commitment", OthName = "", CompetencyCategoryId = 3, Description = "", SortOrder = 130, IsActive = true },
                    new CompetencySubCategory { Name = "Task Orientation", OthName = "", CompetencyCategoryId = 3, Description = "", SortOrder = 140, IsActive = true },
                    new CompetencySubCategory { Name = "Positive Attitude", OthName = "", CompetencyCategoryId = 3, Description = "", SortOrder = 150, IsActive = true },
                    new CompetencySubCategory { Name = "Self Development", OthName = "", CompetencyCategoryId = 3, Description = "", SortOrder = 160, IsActive = true },
                    new CompetencySubCategory { Name = "Problem Understanding and Defining", OthName = "", CompetencyCategoryId = 4, Description = "", SortOrder = 170, IsActive = true },
                    new CompetencySubCategory { Name = "Analytical Ability", OthName = "", CompetencyCategoryId = 4, Description = "", SortOrder = 180, IsActive = true },
                    new CompetencySubCategory { Name = "Decision Making", OthName = "", CompetencyCategoryId = 4, Description = "", SortOrder = 190, IsActive = true },
                    new CompetencySubCategory { Name = "Data Interpretation", OthName = "", CompetencyCategoryId = 4, Description = "", SortOrder = 200, IsActive = true },
                    new CompetencySubCategory { Name = "Creativity", OthName = "", CompetencyCategoryId = 4, Description = "", SortOrder = 210, IsActive = true },
                    new CompetencySubCategory { Name = "Leadership Skills", OthName = "", CompetencyCategoryId = 5, Description = "", SortOrder = 220, IsActive = true },
                    new CompetencySubCategory { Name = "Delegation Skills", OthName = "", CompetencyCategoryId = 5, Description = "", SortOrder = 230, IsActive = true },
                    new CompetencySubCategory { Name = "People Management", OthName = "", CompetencyCategoryId = 5, Description = "", SortOrder = 240, IsActive = true },
                    new CompetencySubCategory { Name = "HR Orientation", OthName = "", CompetencyCategoryId = 5, Description = "", SortOrder = 250, IsActive = true },
                    new CompetencySubCategory { Name = "Coaching and Mentoring", OthName = "", CompetencyCategoryId = 5, Description = "", SortOrder = 260, IsActive = true },
                    new CompetencySubCategory { Name = "Communication Skills", OthName = "", CompetencyCategoryId = 6, Description = "", SortOrder = 270, IsActive = true },
                    new CompetencySubCategory { Name = "Service Orientation", OthName = "", CompetencyCategoryId = 6, Description = "", SortOrder = 280, IsActive = true },
                    new CompetencySubCategory { Name = "Selling Skills", OthName = "", CompetencyCategoryId = 6, Description = "", SortOrder = 290, IsActive = true },
                    new CompetencySubCategory { Name = "Negotiation Skills", OthName = "", CompetencyCategoryId = 6, Description = "", SortOrder = 300, IsActive = true },
                    new CompetencySubCategory { Name = "Interpersonal Skills", OthName = "", CompetencyCategoryId = 6, Description = "", SortOrder = 310, IsActive = true },
                    new CompetencySubCategory { Name = "Technical Knowledge", OthName = "", CompetencyCategoryId = 7, Description = "", SortOrder = 320, IsActive = true },
                    new CompetencySubCategory { Name = "Technology Orientation", OthName = "", CompetencyCategoryId = 7, Description = "", SortOrder = 330, IsActive = true },
                    new CompetencySubCategory { Name = "Quality Orientation", OthName = "", CompetencyCategoryId = 7, Description = "", SortOrder = 340, IsActive = true },
                    new CompetencySubCategory { Name = "Process and Cost Orientation", OthName = "", CompetencyCategoryId = 7, Description = "", SortOrder = 350, IsActive = true },
                    new CompetencySubCategory { Name = "Time Management", OthName = "", CompetencyCategoryId = 7, Description = "", SortOrder = 360, IsActive = true },
                    new CompetencySubCategory { Name = "Degree", OthName = "", CompetencyCategoryId = 8, Description = "", SortOrder = 1, IsActive = true }
                };
                foreach (CompetencySubCategory x in compSubCats)
                    _context.CompetencySubCategories.Add(x);
                _context.SaveChanges();
            }

            if (!_context.EmploymentTypes.Any())
            {
                var employmentTypes = new EmploymentType[]
                {
                    new EmploymentType { Name = "Full Time Job", OthName = "Full Time Job", SysCode = "FULL_TIME", SortOrder = 10, IsActive = true },
                    new EmploymentType { Name = "Part Time Job", OthName = "Part Time Job", SysCode = "PART_TIME", SortOrder = 20, IsActive = true },
                    new EmploymentType { Name = "Consultant", OthName = "Consultant", SysCode = "CONSULTANT", SortOrder = 30, IsActive = true },
                    new EmploymentType { Name = "Contractor", OthName = "Contractor", SysCode = "CONTRACTOR", SortOrder = 40, IsActive = true },
                    new EmploymentType { Name = "Daily", OthName = "Daily", SysCode = "DAILY", SortOrder = 50, IsActive = true }
                };
                foreach (EmploymentType x in employmentTypes)
                    _context.EmploymentTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.PromotionTypes.Any())
            {
                var promotionTypes = new PromotionType[]
                {
                    new PromotionType { Name = "Promotion", OthName = "Promotion", SysCode = "PROMO", SortOrder = 10, IsActive = true },
                    new PromotionType { Name = "Salary Increase", OthName = "Salary Increase", SysCode = "SAL_INCR", SortOrder = 20, IsActive = true },
                    new PromotionType { Name = "Transfer", OthName = "Transfer", SysCode = "TRANS", SortOrder = 30, IsActive = true }
                };
                foreach (PromotionType x in promotionTypes)
                    _context.PromotionTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.DocumentTypes.Any())
            {
                var documentTypes = new DocumentType[]
                {
                    new DocumentType { Name = "National Id", OthName = "National Id", SysCode = "NID", SortOrder = 10, IsActive = true },
                    new DocumentType { Name = "Passport", OthName = "Passport", SysCode = "PASSPORT", SortOrder = 20, IsActive = true },
                    new DocumentType { Name = "Driving Licence", OthName = "Driving Licence", SysCode = "DRIVE", SortOrder = 30, IsActive = true },
                    new DocumentType { Name = "Work Permit", OthName = "Work Permit", SysCode = "WORK", SortOrder = 40, IsActive = true }
                };
                foreach (DocumentType x in documentTypes)
                    _context.DocumentTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.FamilyMemberTypes.Any())
            {
                var familyMemberTypes = new FamilyMemberType[]
                {
                    new FamilyMemberType { Name = "Spouse", OthName = "Spouse", SysCode = "SPOUSE", SortOrder = 10, IsActive = true },
                    new FamilyMemberType { Name = "Son", OthName = "Son", SysCode = "SON", SortOrder = 20, IsActive = true },
                    new FamilyMemberType { Name = "Daughter", OthName = "Daughter", SysCode = "DAUGHTER", SortOrder = 30, IsActive = true }
                };
                foreach (FamilyMemberType x in familyMemberTypes)
                    _context.FamilyMemberTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.LanguageTypes.Any())
            {
                var languageTypes = new LanguageType[]
                {
                    new LanguageType { Name = "English", OthName = "English", SysCode = "EN", SortOrder = 10, IsActive = true },
                    new LanguageType { Name = "French", OthName = "French", SysCode = "FR", SortOrder = 20, IsActive = true },
                    new LanguageType { Name = "Arabic", OthName = "Arabic", SysCode = "AR", SortOrder = 30, IsActive = true },
                    new LanguageType { Name = "Other", OthName = "Other", SysCode = "OTHER", SortOrder = 40, IsActive = true }
                };
                foreach (LanguageType x in languageTypes)
                    _context.LanguageTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.QualificationTypes.Any())
            {
                var qualificationTypes = new QualificationType[]
                {
                    new QualificationType { Name = "BSc", OthName = "BSc", SysCode = "BSC", SortOrder = 10, IsActive = true },
                    new QualificationType { Name = "MSc", OthName = "MSc", SysCode = "MSC", SortOrder = 20, IsActive = true },
                    new QualificationType { Name = "PhD", OthName = "PhD", SysCode = "PHD", SortOrder = 30, IsActive = true },
                    new QualificationType { Name = "Institute", OthName = "Institute", SysCode = "INSTITUTE", SortOrder = 40, IsActive = true },
                    new QualificationType { Name = "High School", OthName = "High School", SysCode = "HIGH_SCHOOL", SortOrder = 50, IsActive = true },
                    new QualificationType { Name = "Professional Diplome", OthName = "Professional Diplome", SysCode = "PROF_DIPLOME", SortOrder = 60, IsActive = true },
                    new QualificationType { Name = "Training Certificate", OthName = "Training Certificate", SysCode = "TRAINING", SortOrder = 70, IsActive = true },
                    new QualificationType { Name = "Domain Experience", OthName = "Domain Experience", SysCode = "DOMAIN_EXPERIENCE", SortOrder = 80, IsActive = true },
                    new QualificationType { Name = "Other Experiences", OthName = "Other Experiences", SysCode = "OTHER_EXPERIENCE", SortOrder = 90, IsActive = true },
                    new QualificationType { Name = "Other Qualifications", OthName = "Other Qualifications", SysCode = "OTHER", SortOrder = 100, IsActive = true }
                };
                foreach (QualificationType x in qualificationTypes)
                    _context.QualificationTypes.Add(x);
                _context.SaveChanges();
            }

            if (!_context.Governorates.Any())
            {
                var governorates = new Governorate[]
                {
                    new Governorate { Name = "Damascus", OthName ="دمشق", SortOrder = 10, IsActive = true },
                    new Governorate { Name = "Damascus Countryside", OthName ="ريف دمشق", SortOrder = 20, IsActive = true },
                    new Governorate { Name = "Aleppo", OthName ="حلب", SortOrder = 30, IsActive = true },
                    new Governorate { Name = "Homs", OthName ="حمص", SortOrder = 40, IsActive = true },
                    new Governorate { Name = "Hama", OthName ="حماة", SortOrder = 50, IsActive = true },
                    new Governorate { Name = "Tartous", OthName ="طرطوس", SortOrder = 60, IsActive = true },
                    new Governorate { Name = "Latakieh", OthName ="اللاذقية", SortOrder = 70, IsActive = true },
                    new Governorate { Name = "Idleb", OthName ="إدلب", SortOrder = 80, IsActive = true },
                    new Governorate { Name = "Hasakah", OthName ="الحسكة", SortOrder = 90, IsActive = true },
                    new Governorate { Name = "Deir Ezzore", OthName ="دير الزور", SortOrder = 100, IsActive = true },
                    new Governorate { Name = "Rigga", OthName ="الرقة", SortOrder = 110, IsActive = true },
                    new Governorate { Name = "Daraa", OthName ="درعا", SortOrder = 120, IsActive = true },
                    new Governorate { Name = "Sweida", OthName ="السويداء", SortOrder = 130, IsActive = true },
                    new Governorate { Name = "Kuneitrah", OthName ="القنيطرة", SortOrder = 140, IsActive = true }
                };
                foreach (Governorate x in governorates)
                    _context.Governorates.Add(x);
                _context.SaveChanges();
            }

            if (!_context.Nationalities.Any())
            {
                var nationalities = new Nationality[]
                {
                    new Nationality { Name = "Syrian Citizen", OthName = "Syrian Citizen", SortOrder = 10, IsActive = true },
                    new Nationality { Name = "Syrian Privilleged", OthName = "Syrian Privilleged", SortOrder = 20, IsActive = true },
                    new Nationality { Name = "Arab", OthName = "Arab", SortOrder = 30, IsActive = true },
                    new Nationality { Name = "Indian", OthName = "Indian", SortOrder = 40, IsActive = true },
                    new Nationality { Name = "Western", OthName = "Western", SortOrder = 50, IsActive = true }
                };
                foreach (Nationality x in nationalities)
                    _context.Nationalities.Add(x);
                _context.SaveChanges();
            }
        }
    }
}
