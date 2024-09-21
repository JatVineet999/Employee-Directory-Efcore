using Application.DTO;

namespace Application.Interfaces
{
    public interface IDepartmentAndRolesServices
    {
        bool AddRoleToDepartment(int departmentId, string newRole);
        List<DepartmentAndRolesDto> GetDepartmentsAndRoles();
    }
}
