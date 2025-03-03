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
        public IEnumerable<Employee> Get()
        {
            return _dbContext.Employees;
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            Employee employee = _dbContext.Employees.FirstOrDefault(e => e.Id == id);
            return employee;
        }

        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee employee)
        {
            Employee employeeFromDb = _dbContext.Employees.Find(id);

            employeeFromDb.FirstName = employee.FirstName;
            employeeFromDb.LastName = employee.LastName;
            employeeFromDb.Gender = employee.Gender;
            employeeFromDb.PositionId = employee.PositionId;
            employeeFromDb.DepartmentId = employee.DepartmentId;
            employeeFromDb.HireDate = employee.HireDate;
            employeeFromDb.Salary = employee.Salary;
            employeeFromDb.EmployeeStatus = employee.EmployeeStatus;
            _dbContext.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Employee employeeToBeDeleted = _dbContext.Employees.Find(id);
            _dbContext.Employees.Remove(employeeToBeDeleted);
            _dbContext.SaveChanges();
        }
    }
}
