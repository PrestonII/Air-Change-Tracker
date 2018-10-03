using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirChangeTracer.DomainObjects;

namespace AirChangeTracer.Services
{
    public class LookupService
    {
        public void ReadCSVToList(string path)
        {

            OccupancyLookup thing = null;
            var reader = File.OpenText(path);
            var csv = new CsvReader(reader);

            while (csv.Read())
            {
                if (csv.Context.Row <= 4)
                    continue;

                var row = csv.Context.Record;

                
            }
        }
    }
}
