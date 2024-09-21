using Infrastructure.Models;
using Application.DTO;
namespace Application.Interfaces
{
    public interface IEmployeeServices
    {
        bool AddEmployeeRecord(EmployeeDto employee);
        AddEmployeeDto FetchEmployeeByID(string employeeID);
        bool SaveUpdatedEmployeeData<T>(AddEmployeeDto employee, T userInput, string propertyType);
        List<EmployeeDto> GetEmployeeRecords();
        bool DeleteEmployee(string EmployeeID);
    }
}
