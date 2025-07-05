using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MirTechTestovoe.Server.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Department { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime BirthDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EmploymentDate { get; set; }

        public int Salary { get; set; }
    }

    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Department { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string EmploymentDate { get; set; } = string.Empty;
        public int Salary { get; set; }
    }

    public class CreateEmployeeDto
    {
        public string Department { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public int Salary { get; set; }
    }

    public class UpdateEmployeeDto
    {
        public string? Department { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public int? Salary { get; set; }
    }
}
