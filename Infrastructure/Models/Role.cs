namespace Infrastructure.Models
{
    public class Role
    {
        public int RoleID { get; set; }  
        public string? RoleName { get; set; }
        public int DepartmentID { get; set; }

        // Navigation property
        public Department? Department { get; set; }
    }
}
