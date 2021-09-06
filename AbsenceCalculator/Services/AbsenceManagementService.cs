using AbsenceCalculator.Models;
using AbsenceCalculator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceCalculator.Services
{
    public interface IAbsneceManagementService
    {
        List<EmployeesViewModel> GetEmployeesWithAbsence(List<Employee> employees, List<Absence> absences);
        EmployeeWithAbsencesViewModel GetEmployeeWithItsAbsences(List<Employee> employees, List<Absence> absences, int employeeId);
    }
    public class AbsenceManagementService : IAbsneceManagementService
    {
        public List<EmployeesViewModel> GetEmployeesWithAbsence(List<Employee> employees, List<Absence> absences)
        {
            var employeeIdWithTotalDaysAbsent = new Dictionary<int, int>();
            foreach (var absence in absences)
            {
                if (!employeeIdWithTotalDaysAbsent.ContainsKey(absence.EmployeeId))
                {
                    employeeIdWithTotalDaysAbsent.Add(absence.EmployeeId, (absence.EndDate - absence.StartDate).Days);
                }
                else
                {
                    employeeIdWithTotalDaysAbsent[absence.EmployeeId] += (absence.EndDate - absence.StartDate).Days;
                }
            }


            var employeesViewModel = employeeIdWithTotalDaysAbsent.Select(e => new EmployeesViewModel
            {
                TotalNumberOfDaysAbsent = e.Value,
                EmployeeName = employees.First(a => a.EmployeeNumber == e.Key).Name
            });

            return employeesViewModel.ToList();
        }

        public EmployeeWithAbsencesViewModel GetEmployeeWithItsAbsences(List<Employee> employees, List<Absence> absences, int employeeId)
        {

            var employeeWithPeriodsOfAbsnece = new Dictionary<int, List<PeriodOfAbsenceViewModel>>();
            foreach (var absence in absences)
            {
                if (!employeeWithPeriodsOfAbsnece.ContainsKey(absence.EmployeeId))
                {
                    employeeWithPeriodsOfAbsnece.Add(absence.EmployeeId, new List<PeriodOfAbsenceViewModel>() { new PeriodOfAbsenceViewModel { From = absence.StartDate, To = absence.EndDate } });
                }
                else
                {
                    employeeWithPeriodsOfAbsnece[absence.EmployeeId].Add(new PeriodOfAbsenceViewModel { From = absence.StartDate, To = absence.EndDate });
                }
            }
            var employeesWithPeriodOfAbsneceViewModel = employeeWithPeriodsOfAbsnece.Where(e => e.Key == employeeId).Select(e => new EmployeeWithAbsencesViewModel
            {
                Periods = e.Value,
                Name = employees.First(a => a.EmployeeNumber == e.Key).Name
            }).First();

            return employeesWithPeriodOfAbsneceViewModel;

        }



    }
}
