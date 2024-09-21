using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IEmployeeRepo 
    {
        List<Employee> LoadEmployeeRecords();
        bool SaveEmployeeRecord(Employee employee);
        bool RemoveEmployee(string EmployeeID);
        bool UpdateEmployee(Employee updatedEmployee);
        Employee GetEmployeeByEmployeeID(string employeeID);


    }
}
