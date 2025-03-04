using EmployeeRecords.Data;
using EmployeeRecords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get()
        {
            return Ok(_dbContext.Employees);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Employee employee = _dbContext.Employees.FirstOrDefault(e => e.Id == id);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            Employee employeeFromDb = _dbContext.Employees.Find(id);

            if (employeeFromDb == null) return NotFound();
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
                _dbContext.SaveChanges();
                return Ok("The record has been updated successfully");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Employee employeeToBeDeleted = _dbContext.Employees.Find(id);
            if (employeeToBeDeleted == null) return NotFound();
            else
            {
                _dbContext.Employees.Remove(employeeToBeDeleted);
                _dbContext.SaveChanges();
                return Ok("The record has been deleted successfully");
            }
        }
    }
}
