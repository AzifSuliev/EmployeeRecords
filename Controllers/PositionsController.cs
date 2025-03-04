using EmployeeRecords.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get()
        {
            return Ok(_dbContext.Positions);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Position position = _dbContext.Positions.Find(id);
            return Ok(position);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Position position)
        {
            _dbContext.Positions.Add(position);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Position position)
        {
            Position positionFromDb = _dbContext.Positions.Find(id);
            if (positionFromDb == null) return NotFound();
            else
            {
                positionFromDb.Title = position.Title;
                positionFromDb.SalaryRangeMin = position.SalaryRangeMin;
                positionFromDb.SalaryRangeMax = position.SalaryRangeMax;
                _dbContext.SaveChanges();
                return Ok("The record has been updated successfully");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Position positionToBeDeleted = _dbContext.Positions.Find(id);
            if (positionToBeDeleted == null) return NotFound();
            else
            {
                _dbContext.Positions.Remove(positionToBeDeleted);
                _dbContext.SaveChanges();
                return Ok("The record has been deleted successfully");
            }
        }
    }
}
