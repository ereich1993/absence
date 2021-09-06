using AbsenceCalculator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceCalculator.Services
{
    public class EmployeeReaderService : ICsvService<string, List<Employee>>
    {
        public List<Employee> ReadCsv(string path)
        {
            var employees = new List<Employee>();
            if (path == null)
            {
                throw new ArgumentException();
            }

            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var splitLine = line.Split(",");
                    employees.Add(new Employee
                    {
                        Id = int.Parse(splitLine[0]),
                        EmployeeNumber = int.Parse(splitLine[1]),
                        Name = splitLine[2],
                        Department = splitLine[3]


                    });
                }
            }
            return employees;
        }
    }
}
