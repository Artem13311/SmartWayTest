using Dapper;
using SmartWayTest.Models;
using SmartWayTest.Models.Data;
using System.Data;

namespace SmartWayTest.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperDBContext _context;

        public EmployeeRepository(DapperDBContext context)
        {
            _context = context;
        }

        

        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            string query = @"Select Employees.Id,Employees.Name,Employees.Surname,Employees.Phone,Employees.CompanyId,
                Passports.Type,Passports.Number,
                Departments.Name,Departments.Phone
                from Employees 
                INNER JOIN Passports on Employees.Id = Passports.EmployeeId
                INNER JOIN Departments ON Employees.Id=Departments.DepartmentId;";


            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee,Passport, Department, Employee>(query, (employees, passport, department) =>
                {
                    employees.Passport = passport;
                    employees.Department = department;
                    return employees;
                }, splitOn: "Type,Name");
                return employees;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByCompanyId(int id)
        {
            string query = @"Select Employees.Id,Employees.Name,Employees.Surname,Employees.Phone,Employees.CompanyId,
                Passports.Type,Passports.Number,
                Departments.Name,Departments.Phone
                from Employees 
                INNER JOIN Passports on Employees.Id = Passports.EmployeeId
                INNER JOIN Departments ON Employees.Id=Departments.DepartmentId WHERE CompanyId = @id;";


            using (var connection = _context.CreateConnection())
            {
                var p = new
                {
                    id = id,
                };
                var employees = await connection.QueryAsync<Employee, Passport, Department, Employee>(query, (employees, passport, department) =>
                {
                    employees.Passport = passport;
                    employees.Department = department;
                    return employees;
                }, splitOn: "Type,Name",param:p);
                return employees;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByCompanyIdAndDepartment(int id, string name)
        {

            string query = @"Select Employees.Id,Employees.Name,Employees.Surname,Employees.Phone,Employees.CompanyId,
                Passports.Type,Passports.Number,
                Departments.Name,Departments.Phone
                from Employees 
                INNER JOIN Passports on Employees.Id = Passports.EmployeeId
                INNER JOIN Departments ON Employees.Id=Departments.DepartmentId WHERE CompanyId = @id and Departments.Name = @name;";


            using (var connection = _context.CreateConnection())
            {
                var p = new
                {
                    id = id,
                    name = name,
                };
                var employees = await connection.QueryAsync<Employee, Passport, Department, Employee>(query, (employees, passport, department) =>
                {
                    employees.Passport = passport;
                    employees.Department = department;
                    return employees;
                }, splitOn: "Type,Name", param: p);
                return employees;
            }
        }

        public async Task<string> CreateEmployee(Employee employee)
        {
            string response = string.Empty;
            string query = @"
                        INSERT INTO Employees VALUES(@name,@surname,@phone,@companyId);
                        INSERT INTO Passports VALUES(@type,@number);
                        INSERT INTO Departments VALUES(@nameDepartment,@phoneDepartment);";
            var p = new DynamicParameters();
            p.Add("name", employee.Name, DbType.String);
            p.Add("surname", employee.Surname, DbType.String);
            p.Add("phone", employee.Phone, DbType.String);
            p.Add("companyId", employee.CompanyId, DbType.Int32);
            p.Add("type", employee.Passport?.Type, DbType.String);
            p.Add("number", employee.Passport?.Number, DbType.String);
            p.Add("nameDepartment", employee.Department?.Name, DbType.String);
            p.Add("phoneDepartment", employee.Department?.Phone, DbType.String);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, p);
                response = employee.Name;
            }
            return response;

        }


        public async Task<string> DeleteEmployee(int id)
        {
            
            string response = string.Empty;
            string query = @"DELETE FROM Employees WHERE Id = @id;
                             DELETE FROM Passports WHERE EmployeeId = @id;
                             DELETE FROM Departments WHERE DepartmentId = @id;";



            using (var connection = _context.CreateConnection())
            {
                var p = new
                {
                    id = id
                };
                await connection.ExecuteAsync(query, p);

                response = "wohoo!";
            }
            return response;    
        }


        public async Task<string> UpdateEmployee(Employee employee,int id)
        {
            string response = string.Empty;
            string query = @"Update Employees Set Name=@name,Surname=@surname,Phone=@phone,CompanyId=@companyId where Id=@id;
                             Update Passports Set Type=@type,Number=@number WHERE EmployeeId=@id;
                             Update Departments Set Name=@nameDepartment,Phone=@phoneDepartment WHERE DepartmentId=@id;";
                        
            var p = new DynamicParameters();
            p.Add("id", id, DbType.Int32);
            p.Add("name", employee.Name, DbType.String);
            p.Add("surname", employee.Surname, DbType.String);
            p.Add("phone", employee.Phone, DbType.String);
            p.Add("companyId", employee.CompanyId, DbType.Int32);
            p.Add("type", employee.Passport?.Type, DbType.String);
            p.Add("number", employee.Passport?.Number, DbType.String);
            p.Add("nameDepartment", employee.Department?.Name, DbType.String);
            p.Add("phoneDepartment", employee.Department?.Phone, DbType.String);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, p);
                response = "wohoo";
            }
            return response;


        }

    }
}
