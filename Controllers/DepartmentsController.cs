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
        public IEnumerable<Department> Get()
        {
            return _dbContext.Departments;
        }

        [HttpGet("{id}")]
        public Department Get(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            return department;
        }

        [HttpPost]
        public void Post([FromBody] Department department)
        {
            _dbContext.Departments.Add(department);
            _dbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Department department)
        {
            Department departmentFromDb = _dbContext.Departments.Find(id);

            departmentFromDb.Name = department.Name;
            departmentFromDb.ManagerId = department.ManagerId;

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Department departmentToBeDeleted = _dbContext.Departments.Find(id);
            _dbContext.Departments.Remove(departmentToBeDeleted);
            _dbContext.SaveChanges();
        }
    }
}
