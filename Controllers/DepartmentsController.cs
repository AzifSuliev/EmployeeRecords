using EmployeeRecords.Data;
using EmployeeRecords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace EmployeeRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public DepartmentsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Departments);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            return Ok(department);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Department department)
        {
            _dbContext.Departments.Add(department);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Department department)
        {
            Department departmentFromDb = _dbContext.Departments.Find(id);
            if (departmentFromDb == null) return NotFound();
            else
            {
                departmentFromDb.Name = department.Name;
                departmentFromDb.ManagerId = department.ManagerId;
                return Ok("The record has been updated successfully");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Department departmentToBeDeleted = _dbContext.Departments.Find(id);
            if (departmentToBeDeleted == null) return NotFound();
            else
            {
                _dbContext.Departments.Remove(departmentToBeDeleted);
                _dbContext.SaveChanges();
                return Ok("The record has been deleted successfully");
            }
        }
    }
}
