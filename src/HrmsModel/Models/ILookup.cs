namespace HrmsModel.Models
{
    public interface ILookup
    {
        int Id { get; set; }
        string Name { get; set; }
        string OthName { get; set; }
        string SysCode { get; set; }
        int SortOrder { get; set; }
        bool IsActive { get; set; }
    }
}
