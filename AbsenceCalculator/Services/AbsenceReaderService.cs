using AbsenceCalculator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceCalculator.Services
{
    public class AbsenceReaderService : ICsvService<string, List<Absence>>
    {
        public List<Absence> ReadCsv(string path)
        {
            var asbences = new List<Absence>();
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
                    
                    asbences.Add(new Absence 
                    {
                        Id = int.Parse(splitLine[0]),
                        StartDate = DateTime.ParseExact(splitLine[1], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        EndDate = DateTime.ParseExact(splitLine[2], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Type = int.Parse(splitLine[3]),
                        EmployeeId = int.Parse(splitLine[4])
                    });

                }
            }
            return asbences;
        }
    }
}
