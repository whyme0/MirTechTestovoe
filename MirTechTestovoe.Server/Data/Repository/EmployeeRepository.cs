using MirTechTestovoe.Server.Models;

namespace MirTechTestovoe.Server.Data.Repository
{    
    public interface IEmployeeRepository: IRepository<Employee, CreateEmployeeDto, UpdateEmployeeDto>
    {
        IEnumerable<Employee> GetAll(string? sortBy, bool descending);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll(string? sortBy, bool descending)
        {
            IQueryable<Employee> query = _context.Employees;
            if (string.IsNullOrEmpty(sortBy))
            {
                query = descending ? query.OrderByDescending(e => e.Id) : query.OrderBy(e => e.Id);
            }
            else
            {
                switch (sortBy.ToLower())
                {
                    case "department":
                        query = descending ? query.OrderByDescending(e => e.Department) : query.OrderBy(e => e.Department);
                        break;
                    case "fullname":
                        query = descending ? query.OrderByDescending(e => e.FullName) : query.OrderBy(e => e.FullName);
                        break;
                    case "birthdate":
                        query = descending ? query.OrderByDescending(e => e.BirthDate) : query.OrderBy(e => e.BirthDate);
                        break;
                    case "employmentdate":
                        query = descending ? query.OrderByDescending(e => e.EmploymentDate) : query.OrderBy(e => e.EmploymentDate);
                        break;
                    case "salary":
                        query = descending ? query.OrderByDescending(e => e.Salary) : query.OrderBy(e => e.Salary);
                        break;
                }
            }
            return query.ToList();
        }

        public Employee? GetById(int id) => _context.Employees.Find(id);
        public Employee Add(CreateEmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Department = employeeDto.Department,
                FullName = employeeDto.FullName,
                BirthDate = employeeDto.BirthDate ?? DateTime.MinValue,
                EmploymentDate = employeeDto.EmploymentDate ?? DateTime.MinValue,
                Salary = employeeDto.Salary
            };
            
            _context.Employees.Add(employee);
            SaveChanges();
            
            return employee;
        }

        public Employee? Update(int id, UpdateEmployeeDto employeeDto)
        {
            var existing = GetById(id);
            if (existing == null)
                return null;

            bool updated = false;
            if (!string.IsNullOrEmpty(employeeDto.Department) && existing.Department != employeeDto.Department)
            {
                existing.Department = employeeDto.Department;
                updated = true;
            }
            if (!string.IsNullOrEmpty(employeeDto.FullName) && existing.FullName != employeeDto.FullName)
            {
                existing.FullName = employeeDto.FullName;
                updated = true;
            }
            if (employeeDto.BirthDate.HasValue && existing.BirthDate != employeeDto.BirthDate.Value)
            {
                existing.BirthDate = employeeDto.BirthDate.Value;
                updated = true;
            }
            if (employeeDto.EmploymentDate.HasValue && existing.EmploymentDate != employeeDto.EmploymentDate.Value)
            {
                existing.EmploymentDate = employeeDto.EmploymentDate.Value;
                updated = true;
            }
            if (employeeDto.Salary.HasValue && existing.Salary != employeeDto.Salary.Value)
            {
                existing.Salary = employeeDto.Salary.Value;
                updated = true;
            }
            if (updated)
                SaveChanges();
            return existing;
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            SaveChanges();
        }
        public void SaveChanges() => _context.SaveChanges();
    }
}
