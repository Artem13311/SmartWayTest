using SmartWayTest.Models;

namespace SmartWayTest.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<IEnumerable<Employee>> GetEmployeeByCompanyId(int id);
        Task<IEnumerable<Employee>> GetEmployeeByCompanyIdAndDepartment(int id,string name);
        Task<string> CreateEmployee(Employee employee);
        Task<string> DeleteEmployee(int id);
        Task<string> UpdateEmployee(Employee employee, int id);




    }
}
