using Infrastructure.Db_Context;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Repos
{
    public class DepartmentsRepo : IDepartmentsRepo
    {
        internal readonly EmployeeDirectoryDbContext _context;

        public DepartmentsRepo(EmployeeDirectoryDbContext context)
        {
            _context = context;
        }
        public bool SaveNewRole(int departmentId, string newRole)
        {
            try
            {
                _context.Roles?.Add(new Role { RoleName = newRole, DepartmentID = departmentId });
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the new role.", ex);
            }
        }
        public List<(Department, List<Role>)> LoadDepartmentsAndRoles()
        {
            try
            {
                var departmentsAndRoles = _context.Departments.Select(d => new
                {
                    Department = d,
                    Roles = d.Roles!.ToList()
                }).ToList();

                return departmentsAndRoles.Select(dr => (dr.Department, dr.Roles)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while loading departments and roles.", ex);
            }
        }

    }
}
