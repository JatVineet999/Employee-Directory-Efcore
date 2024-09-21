namespace Application.DTO
{
    public class RoleDto
    {
        public int RoleID { get; set; }
        public string? RoleName { get; set; }
    }

    public class DepartmentDto
    {
        public int DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
    }

    public class DepartmentAndRolesDto
    {
        public DepartmentDto? Department { get; set; }
        public List<RoleDto>? Roles { get; set; }
    }
}
