using Microsoft.AspNetCore.Mvc;
using SmartWayTest.Models;
using SmartWayTest.Repository;

namespace SmartWayTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult>GetAllEmployees()
        {
            try
            {
                var employeeList = await _employeeRepository.GetAllEmployee();

                return Ok(employeeList);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpGet("GetEmployeeByCompanyId{id}")]
        public async Task<IActionResult>GetEmployeeByCompanyId(int id)
        {
            try
            {
                var employeeList = await _employeeRepository.GetEmployeeByCompanyId(id);


                return Ok(employeeList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }


        [HttpGet("GetEmployeeByCompanyIdAndDepartment")]
        public async Task<IActionResult> GetEmployeeByCompanyId(int id,string name)
        {
            try
            {
                var employeeList = await _employeeRepository.GetEmployeeByCompanyIdAndDepartment(id,name);

                return Ok(employeeList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody]Employee employee)
        {
            try
            {
                var result = await _employeeRepository.CreateEmployee(employee);

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }



        }

        [HttpDelete("DeleteEmployee{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.DeleteEmployee(id);

                return Ok(result);
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPut("UpdateEmployee{id}")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee,int id)
        {
            try
            {
                var result = await _employeeRepository.UpdateEmployee(employee, id);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }

    }
}
