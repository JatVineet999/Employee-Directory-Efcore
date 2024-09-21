namespace Infrastructure.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string? DepartmentName { get; set; }

        // Navigation property
        public ICollection<Role>? Roles { get; set; }
    }
}
