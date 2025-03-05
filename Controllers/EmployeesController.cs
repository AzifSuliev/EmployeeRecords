using EmployeeRecords.Data;
using EmployeeRecords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Employees.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Employee employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null) return NotFound("No record has been found against this ID");
            return Ok(employee);
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
