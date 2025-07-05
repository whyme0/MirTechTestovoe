using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MirTechTestovoe.Server.Data.Repository;
using MirTechTestovoe.Server.Models;
using System.Globalization;

namespace MirTechTestovoe.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _repository;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("/employees", Name = "GetEmployees")]
        public IEnumerable<EmployeeDto> GetAll([FromQuery] string? sortBy = "", [FromQuery] bool descending = false)
        {
            return _repository.GetAll(sortBy, descending).Select(e => new EmployeeDto
            {
                Id = e.Id,
                Department = e.Department,
                FullName = e.FullName,
                BirthDate = e.BirthDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                EmploymentDate = e.EmploymentDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                Salary = e.Salary
            });
        }

        [HttpPost("/employees", Name = "CreateEmployee")]
        public IActionResult Create([FromBody] CreateEmployeeDto employeeDto)
        {
            if (employeeDto == null ||
                string.IsNullOrWhiteSpace(employeeDto.Department) ||
                string.IsNullOrWhiteSpace(employeeDto.FullName) ||
                !employeeDto.BirthDate.HasValue ||
                !employeeDto.EmploymentDate.HasValue)
            {
                return BadRequest("Invalid employee data.");
            }

            var employee = _repository.Add(employeeDto);

            var result = new EmployeeDto
            {
                Id = employee.Id,
                Department = employee.Department,
                FullName = employee.FullName,
                BirthDate = employee.BirthDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                EmploymentDate = employee.EmploymentDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                Salary = employee.Salary
            };

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("/employees/{id}", Name = "UpdateEmployee")]
        public IActionResult Update(int id, [FromBody] UpdateEmployeeDto employee)
        {
            if (_repository.GetById(id) == null)
                return NotFound();
            
            var updated = _repository.Update(id, employee);
            
            return StatusCode(StatusCodes.Status202Accepted, updated);
        }

        [HttpDelete("/employees/{id}", Name = "DeleteEmployee")]
        public IActionResult Delete(int id)
        {
            var employee = _repository.GetById(id);
            
            if (employee == null)
                return NotFound();

            _repository.Delete(employee);
            
            return NoContent();
        }
    }
}
