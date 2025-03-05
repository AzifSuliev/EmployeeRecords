using EmployeeRecords.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public PositionsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Positions.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Position position = await _dbContext.Positions.FindAsync(id);
            if(position == null) return NotFound("No record has been found against this ID"); 
            return Ok(position);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Position position)
        {
            await _dbContext.Positions.AddAsync(position);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Position position)
        {
            Position positionFromDb = await _dbContext.Positions.FindAsync(id);
            if (positionFromDb == null) return NotFound("No record has been found against this ID");
            else
            {
                positionFromDb.Title = position.Title;
                positionFromDb.SalaryRangeMin = position.SalaryRangeMin;
                positionFromDb.SalaryRangeMax = position.SalaryRangeMax;
                await _dbContext.SaveChangesAsync();
                return Ok("The record has been updated successfully");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Position positionToBeDeleted = await _dbContext.Positions.FindAsync(id);
            if (positionToBeDeleted == null) return NotFound("No record has been found against this ID");
            else
            {
                _dbContext.Positions.Remove(positionToBeDeleted);
                await _dbContext.SaveChangesAsync();
                return Ok("The record has been deleted successfully");
            }
        }
    }
}
