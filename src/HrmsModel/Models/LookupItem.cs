namespace HrmsModel.Models
{
    public class LookupItem : ILookup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public string SysCode { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
