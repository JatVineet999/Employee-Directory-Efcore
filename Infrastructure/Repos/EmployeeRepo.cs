using Infrastructure.Db_Context;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        internal readonly EmployeeDirectoryDbContext _context;

        public EmployeeRepo(EmployeeDirectoryDbContext context)
        {
            _context = context;
        }
        public List<Employee> LoadEmployeeRecords()
        {
            try
            {
                return _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.Role)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while loading employee records.", ex);
            }
        }

        public bool SaveEmployeeRecord(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the employee record.", ex);
            }
        }

        public bool RemoveEmployee(string EmployeeID)
        {
            try
            {
                var employeeToRemove = _context.Employees.FirstOrDefault(e => e.EmployeeID == EmployeeID);
                if (employeeToRemove != null)
                {
                    _context.Employees.Remove(employeeToRemove);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while removing the employee record.", ex);
            }
        }
        public Employee GetEmployeeByEmployeeID(string employeeID)
        {
            try
            {
                return _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.Role)
                    .FirstOrDefault(e => e.EmployeeID == employeeID)!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the employee record.", ex);
            }
        }


        public bool UpdateEmployee(Employee updatedEmployee)
        {
            try
            {
                var employeeToUpdate = _context.Employees.FirstOrDefault(e => e.EmployeeID == updatedEmployee.EmployeeID);
                if (employeeToUpdate != null)
                {
                    employeeToUpdate.FirstName = updatedEmployee.FirstName;
                    employeeToUpdate.LastName = updatedEmployee.LastName;
                    employeeToUpdate.MobileNumber = updatedEmployee.MobileNumber;
                    employeeToUpdate.Email = updatedEmployee.Email;
                    employeeToUpdate.DateOfBirth = updatedEmployee.DateOfBirth;
                    employeeToUpdate.JoiningDate = updatedEmployee.JoiningDate;
                    employeeToUpdate.Location = updatedEmployee.Location;
                    employeeToUpdate.RoleID = updatedEmployee.RoleID;
                    employeeToUpdate.DepartmentID = updatedEmployee.DepartmentID;
                    employeeToUpdate.ManagerName = updatedEmployee.ManagerName;
                    employeeToUpdate.ProjectName = updatedEmployee.ProjectName;

                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the employee record.", ex);
            }
        }
    }
}
