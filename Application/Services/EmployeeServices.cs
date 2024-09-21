using System.Reflection;
using Infrastructure.Models;
using Application.DTO;
using Application.Interfaces;
using Infrastructure.Interfaces;
using AutoMapper;
namespace Application.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeServices(IEmployeeRepo employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }
        private string GenerateEmployeeID()
        {
            Random rand = new Random();
            return $"TZ{rand.Next(1000, 10000)}";
        }

        public bool AddEmployeeRecord(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.EmployeeID = GenerateEmployeeID();
            bool isAdded = _employeeRepo.SaveEmployeeRecord(employee);
            return isAdded;
        }

        public AddEmployeeDto FetchEmployeeByID(string employeeID)
        {
            var employee = _employeeRepo.GetEmployeeByEmployeeID(employeeID);
            var employeeDto = _mapper.Map<AddEmployeeDto>(employee);
            return employeeDto;
        }
        public bool SaveUpdatedEmployeeData<T>(AddEmployeeDto employee, T userInput, string propertyType)
        {
            var employeeToUpdate = _mapper.Map<Employee>(employee);

            // Getting the PropertyInfo object corresponding to the specified property type of the Employee class.
            PropertyInfo property = typeof(Employee).GetProperty(propertyType)!;
            if (property != null)
            {
                // Assigning the value of the specified property of the employeeToUpdate object.
                property.SetValue(employeeToUpdate, userInput);

                // Update the employee in the repository with the modified data.
                bool isUpdated = _employeeRepo.UpdateEmployee(employeeToUpdate);
                return isUpdated;
            }
            else
            {
                return false;
            }
        }

        public List<EmployeeDto> GetEmployeeRecords()
        {
            var employees = _employeeRepo.LoadEmployeeRecords(); 
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            return employeeDtos;
        }

        public bool DeleteEmployee(string EmployeeID)
        {
            return _employeeRepo.RemoveEmployee(EmployeeID);
        }
    }
}
