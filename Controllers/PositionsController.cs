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
        public IEnumerable<Position> Get()
        {
            return _dbContext.Positions;
        }

        [HttpGet("{id}")]
        public Position Get(int id)
        {
            Position position = _dbContext.Positions.Find(id);
            return position;
        }

        [HttpPost]
        public void Post([FromBody] Position position)
        {
            _dbContext.Positions.Add(position);
            _dbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Position position)
        {
            Position positionFromDb = _dbContext.Positions.Find(id);

            positionFromDb.Title = position.Title;
            positionFromDb.SalaryRangeMin = position.SalaryRangeMin;
            positionFromDb.SalaryRangeMax = position.SalaryRangeMax;
            _dbContext.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Position positionToBeDeleted = _dbContext.Positions.Find(id);
            _dbContext.Positions.Remove(positionToBeDeleted);
            _dbContext.SaveChanges();
        }
    }
}
