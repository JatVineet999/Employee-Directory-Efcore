using Infrastructure.Models;
using Application.Constants;
using Application.Interfaces;
using Presentation.Interfaces;
using Presentation.Constants;
using Application.DTO;

namespace Presentation.MenuManagers
{
    class EmployeesMenuManager : IEmployeesMenuManager
    {
        private readonly IMainMenuManager _mainMenuManager;
        private readonly IInputReader _inputReader;
        private readonly IRolesMenuManager _rolesMenuManager;
        private readonly Dictionary<EmployeesMenuOption, Action> _menuActions;
        private readonly IEmployeeServices _employeeServices;

        public EmployeesMenuManager(IMainMenuManager mainMenuManager, IRolesMenuManager rolesMenuManager, IInputReader InputReader, IEmployeeServices employeeServices)
        {
            _inputReader = InputReader;
            _mainMenuManager = mainMenuManager;
            _rolesMenuManager = rolesMenuManager;
            _employeeServices = employeeServices;
            _menuActions = new Dictionary<EmployeesMenuOption, Action>
            {
                { EmployeesMenuOption.ViewEmployees, ViewEmployees },
                { EmployeesMenuOption.AddEmployee, AddEmployee },
                { EmployeesMenuOption.UpdateEmployee, UpdateEmployee },
                { EmployeesMenuOption.DisplayOne, DisplayEmployeeByNumber },
                { EmployeesMenuOption.DeleteEmployee, DeleteEmployee },
                { EmployeesMenuOption.ReturnToMainMenu, _mainMenuManager.DisplayMainMenu }
            };
        }
        public void EmployeesMenuHandler()
        {
            while (true)
            {
                DisplayMenuOptions();

                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (Enum.TryParse(choice.ToString(), out EmployeesMenuOption selectedOption)
                    && _menuActions.TryGetValue(selectedOption, out Action? option))
                {
                    option.Invoke();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }

                if (!RequestMenuReturnSelection())
                {
                    _mainMenuManager.DisplayMainMenu();
                    return;
                }
            }
        }
        private void DisplayMenuOptions()
        {
            Console.WriteLine("--------------------------------------------------------------Employees Menu-------------------------------------------------------------------------");
            Console.WriteLine("1. View Employees");
            Console.WriteLine("2. Add Employee");
            Console.WriteLine("3. Update Employee");
            Console.WriteLine("4. View Particular Employee Record");
            Console.WriteLine("5. Delete Employee\n");
            Console.WriteLine("Press '0' to Go Back to Previous Menu\n");
        }

        private bool RequestMenuReturnSelection()
        {
            Console.WriteLine("Press '0' to Go Back to Previous Menu options\n                 or\nPress any other key to return to Main Menu");
            char input = Console.ReadKey().KeyChar;
            Console.WriteLine();

            return input == '0';
        }
        private void AddEmployee()
        {
            try
            {
                AddEmployeeDto newEmployee = _GatherEmployeeDetails();

                if (_employeeServices.AddEmployeeRecord(newEmployee))
                {
                    Console.WriteLine("Employee Added Successfully\n");
                }
                else
                {
                    Console.WriteLine("An error occurred while adding the employee record.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding Employee Record: " + ex.Message);
            }
        }


        private void ViewEmployees()
        {
            try
            {
                var employeeRecords = _employeeServices.GetEmployeeRecords();

                PrintEmployeeRecordsHeader();
                foreach (var employee in employeeRecords)
                {
                    DisplayEmployeeRecord(employee);
                }
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while Loading Employee Records: " + ex.Message);
            }
        }

        private void DisplayEmployeeByNumber()
        {
            ViewEmployees();
            Console.WriteLine("Enter Employee Number Of Employee to see only his Record:");
            string EmployeeID = Console.ReadLine()!;
            var employee = _employeeServices.FetchEmployeeByID(EmployeeID);
            if (employee != null)
            {
                PrintEmployeeRecordsHeader();
                DisplayEmployeeRecord(employee);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("Employee with the provided Employee Number does not exist !!!\n");
            }
        }

        private void DisplayEmployeeRecord(EmployeeDto employee)
        {
            Console.WriteLine($"| {employee.EmployeeID,-15} | {employee.FirstName,-18} {employee.LastName,-15} | {employee.JoiningDate.ToString("yyyy-MM-dd"),-12} | {employee.Location,-14} | {employee.JobTitle,-29} | {employee.DepartmentName,-20} | {employee.ManagerName,-14} | {employee.ProjectName,-26} |");
        }


        private void PrintEmployeeRecordsHeader()
        {
            Console.WriteLine("Employee Records:");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| Employee Number | Name                               | Joining Date | Location       | Job Title                     | Department           | Manager Name   | Project Name               |");
            Console.WriteLine("|-----------------|------------------------------------|--------------|----------------|------------------------------ |----------------------|----------------|----------------------------|");
        }

        private void UpdateEmployee()
        {
            try
            {
                Console.WriteLine("Update Employee");
                Console.WriteLine("Please select an employee to update:");
                ViewEmployees();
                Console.Write("Enter Employee Number to update: ");
                string EmployeeIDToUpdate = Console.ReadLine()!;
                var employeeToUpdate = _employeeServices.FetchEmployeeByID(EmployeeIDToUpdate)!;

                if (employeeToUpdate != null)
                {
                    UpdateEmployeeDetails(employeeToUpdate);
                }
                else
                {
                    Console.WriteLine("Employee not found!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured in updating employee Data: {ex.Message}");
            }
        }


        private void UpdateEmployeeDetails(AddEmployeeDto employeeToUpdate)
        {
            Console.WriteLine($"Updating employee: {employeeToUpdate.FirstName} {employeeToUpdate.LastName}");
            Console.WriteLine("Select which information you would like to update:");
            Console.WriteLine("1. First Name");
            Console.WriteLine("2. Last Name");
            Console.WriteLine("3. Mobile Number");
            Console.WriteLine("4. Email");
            Console.WriteLine("5. DateOfBirth");
            Console.WriteLine("6. Joining Date");
            Console.WriteLine("7. Location");
            Console.WriteLine("8. Job Title");
            Console.WriteLine("9. Department");
            Console.WriteLine("10. Manager Name");
            Console.WriteLine("11. Project Name");

            do
            {
                Console.Write("Enter your choice: ");
                string choiceInput = Console.ReadLine()!;
                if (int.TryParse(choiceInput, out int choice))
                {
                    Console.WriteLine();
                    switch (choice)
                    {
                        case 1:
                            UpdateEmployeeProperty(employeeToUpdate, "FirstName", ValidationType.Name);
                            break;
                        case 2:
                            UpdateEmployeeProperty(employeeToUpdate, "LastName", ValidationType.Name);
                            break;
                        case 3:
                            UpdateEmployeeProperty(employeeToUpdate, "MobileNumber", ValidationType.MobileNumber);
                            break;
                        case 4:
                            UpdateEmployeeProperty(employeeToUpdate, "Email", ValidationType.Email);
                            break;
                        case 5:
                            UpdateEmployeeProperty(employeeToUpdate, "DateOfBirth", ValidationType.Date);
                            break;
                        case 6:
                            UpdateEmployeeProperty(employeeToUpdate, "JoiningDate", ValidationType.Date);
                            break;
                        case 7:
                            UpdateEmployeeProperty(employeeToUpdate, "Location", ValidationType.Name);
                            break;
                        case 8:
                            int selectedRoleId = _rolesMenuManager.SelectRoleinDepartment(employeeToUpdate.DepartmentID);
                            if (selectedRoleId != -1)
                            {
                                if (_employeeServices.SaveUpdatedEmployeeData(employeeToUpdate, selectedRoleId, "RoleID"))
                                {
                                    employeeToUpdate.RoleID = selectedRoleId;
                                    Console.WriteLine("RoleID updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("RoleID could not be updated.");
                                }
                            }
                            break;
                        case 9:
                            int selectedDepartmentId = _rolesMenuManager.SelectDepartment();
                            if (selectedDepartmentId != -1)
                            {
                                if (_employeeServices.SaveUpdatedEmployeeData(employeeToUpdate, selectedDepartmentId, "DepartmentID"))
                                {
                                    employeeToUpdate.DepartmentID = selectedDepartmentId;
                                    Console.WriteLine("DepartmentID updated successfully.");
                                    goto case 8;
                                }
                                else
                                {
                                    Console.WriteLine("DepartmentID could not be updated.");
                                }
                            }
                            break;
                        case 10:
                            UpdateEmployeeProperty(employeeToUpdate, "ManagerName", ValidationType.Name);
                            break;
                        case 11:
                            UpdateEmployeeProperty(employeeToUpdate, "ProjectName", ValidationType.Name);
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }

                Console.Write("Do you want to update another detail? (Y/N): ");
                char continueChoice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                if (continueChoice == 'N')
                {
                    break;
                }

            } while (true);
        }


        private void UpdateEmployeeProperty(AddEmployeeDto employee, string propertyName, ValidationType validationType)
        {
            string prompt = $"Enter new {propertyName}: ";
            string input = _inputReader.GetValidInput(prompt, validationType);

            if (_employeeServices.SaveUpdatedEmployeeData(employee, input, propertyName))
            {
                Console.WriteLine($"{propertyName} updated successfully.");
            }
            else
            {
                Console.WriteLine($"{propertyName} could not be updated successfully.");
            }
        }


        private AddEmployeeDto _GatherEmployeeDetails()
        {
            var employee = new AddEmployeeDto();

            employee.FirstName = _inputReader.GetValidInput("Enter First Name: ", ValidationType.Name);
            employee.LastName = _inputReader.GetValidInput("Enter Last Name: ", ValidationType.Name);
            employee.MobileNumber = _inputReader.GetValidInput("Enter Mobile Number: ", ValidationType.MobileNumber);
            employee.Email = _inputReader.GetValidInput("Enter Email: ", ValidationType.Email);
            employee.DateOfBirth = DateTime.Parse(_inputReader.GetValidInput("Enter Date of Birth (yyyy-mm-dd): ", ValidationType.Date));
            employee.JoiningDate = DateTime.Parse(_inputReader.GetValidInput("Enter Joining Date (yyyy-mm-dd): ", ValidationType.Date));
            employee.Location = _inputReader.GetValidInput("Enter Location: ", ValidationType.Name);
            employee.DepartmentID = _rolesMenuManager.SelectDepartment();
            employee.RoleID = _rolesMenuManager.SelectRoleinDepartment(employee.DepartmentID);
            employee.ManagerName = _inputReader.GetValidInput("Enter Manager Name: ", ValidationType.Name);
            employee.ProjectName = _inputReader.GetValidInput("Enter Project Name: ", ValidationType.Name);

            return employee;
        }


        private void DeleteEmployee()
        {
            ViewEmployees();
            Console.Write("Enter the Employee Number of the employee to delete record: ");
            string EmployeeID = Console.ReadLine()!;
            bool deletedSuccessfully = _employeeServices.DeleteEmployee(EmployeeID);
            if (deletedSuccessfully)
            {
                Console.WriteLine($"Employee with Employee Number {EmployeeID} deleted successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found!!!");
            }

        }

    }
}
