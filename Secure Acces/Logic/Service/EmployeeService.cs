using Logic.Dto;
using DAL;

namespace Logic.Services
{
    public class EmployeeService
    {
        private readonly EmployeeDal _employeeDal;

        public EmployeeService(string connectionString)
        {
            _employeeDal = new EmployeeDal(connectionString);
        }

        public DtoEmployee GetEmployee(int employeeId)
        {
            string name = _employeeDal.GetEmployeeNameById(employeeId);
            return new DtoEmployee
            {
                Id = employeeId,
                Name = name
            };
        }
    }
}

