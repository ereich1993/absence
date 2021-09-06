using AbsenceCalculator.Models;
using AbsenceCalculator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceCalculator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IAbsneceManagementService absneceManagementService;
        ICsvService<string, List<Absence>> absenceService;
        ICsvService<string, List<Employee>> employeeService;

        public AbsenceController(IConfiguration configuration, IAbsneceManagementService absneceManagementService, ICsvService<string,List<Absence>> absenceService, ICsvService<string, List<Employee>> employeeService)
        {
            this.configuration = configuration;
            this.absneceManagementService = absneceManagementService;
            this.absenceService = absenceService;
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAbsences()
        {
            var absencesPath = configuration["AbsencesLocation"];
            var employeesPath = configuration["EmployeesLocation"];

            var absences = this.absenceService.ReadCsv(absencesPath);
            var employees = this.employeeService.ReadCsv(employeesPath);

            var viewModel = this.absneceManagementService.GetEmployeesWithAbsence(employees, absences);
            
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAbsenceForEmployee(int id)
        {
            var absencesPath = configuration["AbsencesLocation"];
            var employeesPath = configuration["EmployeesLocation"];

            var absences = this.absenceService.ReadCsv(absencesPath);
            var employees = this.employeeService.ReadCsv(employeesPath);

            var viewModel = this.absneceManagementService.GetEmployeeWithItsAbsences(employees, absences,id);

            return Ok(viewModel);
        }
    }
}
