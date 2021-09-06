using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceCalculator.ViewModels
{
    public class EmployeeWithAbsencesViewModel
    {
        public string Name { get; set; }
        public List<PeriodOfAbsenceViewModel> Periods { get; set; }
    }
}
