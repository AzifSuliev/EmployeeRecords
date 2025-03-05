using EmployeeRecords.Data;
using EmployeeRecords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Departments.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Department department = await _dbContext.Departments.FindAsync(id);
            if (department == null) return NotFound("No record has been found against this ID");
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Department department)
        {
            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Department department)
        {
            Department departmentFromDb = await _dbContext.Departments.FindAsync(id);
            if (departmentFromDb == null) return NotFound("No record has been found against this ID");
            else
            {
                departmentFromDb.Name = department.Name;
                departmentFromDb.ManagerId = department.ManagerId;
                await _dbContext.SaveChangesAsync();
                return Ok("The record has been updated successfully");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Department departmentToBeDeleted = await _dbContext.Departments.FindAsync(id);
            if (departmentToBeDeleted == null) return NotFound("No record has been found against this ID");
            else
            {
                _dbContext.Departments.Remove(departmentToBeDeleted);
                await _dbContext.SaveChangesAsync();
                return Ok("The record has been deleted successfully");
            }
        }
    }
}
