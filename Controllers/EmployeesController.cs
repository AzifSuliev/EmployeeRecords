using EmployeeRecords.Data;
using EmployeeRecords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public EmployeesController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1; // если pageNumber == null то currentPageNumber = 1
            int currentPageSize = pageSize ?? 3; // если pageSize == null то currentPageSize = 3

            var employeesFromDb = await (from employee in _dbContext.Employees
                                         select new
                                         {
                                             Id = employee.Id,
                                             FirstName = employee.FirstName,
                                             LastName = employee.LastName,
                                             Gender = employee.Gender,
                                             HireDate = employee.HireDate,
                                             PositionId = employee.PositionId,
                                             DepartmentId = employee.DepartmentId
                                         }).ToListAsync();

            // Pagination
            return Ok(employeesFromDb.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Employee employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null) return NotFound("No record has been found against this ID");
            return Ok(employee);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchEmployees(string query)
        {
            var employees = await (from employee in _dbContext.Employees
                                   where employee.FirstName.Contains(query) || employee.LastName.Contains(query)
                                   select new
                                   {
                                       Id = employee.Id,
                                       FirstName = employee.FirstName,
                                       LastName = employee.LastName,
                                       Gender = employee.Gender,
                                       HireDate = employee.HireDate,
                                       PositionId = employee.PositionId,
                                       DepartmentId = employee.DepartmentId
                                   }).ToListAsync();
            return Ok(employees);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNewEmployees()
        {
            var newEmployees = await (from employee in _dbContext.Employees
                                      orderby employee.HireDate descending
                                      select new
                                      {
                                          Id = employee.Id,
                                          FirstName = employee.FirstName,
                                          LastName = employee.LastName,
                                          Gender = employee.Gender,
                                          HireDate = employee.HireDate,
                                          PositionId = employee.PositionId,
                                          DepartmentId = employee.DepartmentId
                                      }).Take(3).ToListAsync();
            return Ok(newEmployees);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Employee employee)
        {
            Employee employeeFromDb = await _dbContext.Employees.FindAsync(id);

            if (employeeFromDb == null) return NotFound("No record has been found against this ID");
            else
            {
                employeeFromDb.FirstName = employee.FirstName;
                employeeFromDb.LastName = employee.LastName;
                employeeFromDb.Gender = employee.Gender;
                employeeFromDb.PositionId = employee.PositionId;
                employeeFromDb.DepartmentId = employee.DepartmentId;
                employeeFromDb.HireDate = employee.HireDate;
                employeeFromDb.Salary = employee.Salary;
                employeeFromDb.EmployeeStatus = employee.EmployeeStatus;
                await _dbContext.SaveChangesAsync();
                return Ok("The record has been updated successfully");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Employee employeeToBeDeleted = await _dbContext.Employees.FindAsync(id);
            if (employeeToBeDeleted == null) return NotFound("No record has been found against this ID");
            else
            {
                _dbContext.Employees.Remove(employeeToBeDeleted);
                await _dbContext.SaveChangesAsync();
                return Ok("The record has been deleted successfully");
            }
        }
    }
}
