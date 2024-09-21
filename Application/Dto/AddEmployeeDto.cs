namespace Application.DTO
{
    public class AddEmployeeDto : EmployeeDto
    {
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
    }
}
