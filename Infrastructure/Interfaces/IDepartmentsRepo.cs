using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IDepartmentsRepo 
    {
        bool SaveNewRole(int departmentId, string newRole);
        List<(Department, List<Role>)> LoadDepartmentsAndRoles();
    }
}
