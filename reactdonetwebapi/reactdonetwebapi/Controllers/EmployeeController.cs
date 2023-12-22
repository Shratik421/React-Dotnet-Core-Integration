using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reactdonetwebapi.Models;
using System.Security.Cryptography.X509Certificates;

namespace reactdonetwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
     _employeeDbContext = employeeDbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _employeeDbContext.Employees.ToListAsync();
            if (employees == null)
            { 
                return NotFound();
            }

            return employees;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if (_employeeDbContext.Employees == null)
            {
                return NotFound();
            }
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _employeeDbContext.Employees.Add(employee);
            await _employeeDbContext.SaveChangesAsync();


            return CreatedAtAction(nameof(GetEmployee), new { id = employee.ID }, employee);
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> PutEmployee(int id,Employee employee)
        {
            if(id != employee.ID)
            {
                return BadRequest();

            }

            _employeeDbContext.Entry(employee).State = EntityState.Modified;

            try
            { await _employeeDbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }


        [HttpDelete ("{id}")]
        public async Task<ActionResult> DelteEmployee(int id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

           

            _employeeDbContext.Employees.Remove(employee);
            await _employeeDbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
