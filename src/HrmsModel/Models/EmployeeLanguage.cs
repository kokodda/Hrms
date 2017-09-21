using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeeLanguage
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int LanguageTypeId { get; set; }
        public string LanguageName { get; set; }
        public string ReadingLevel { get; set; }
        public string SpeakingLevel { get; set; }
        public string WritingLevel { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual LanguageType LanguageType { get; set; }
    }
}
