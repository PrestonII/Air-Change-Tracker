using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirChangeTracer.DomainObjects;
using AirChangeTracer.DomainObjects.Ventilation;

namespace AirChangeTracer.Services
{
    public class VentilationLookupService
    {
        public List<OccupancyLookup> ReadCSVToList(string path, int startOfData = 0)
        {
            List<OccupancyLookup> occList = new List<OccupancyLookup>();
            var reader = File.OpenText(path);
            var csv = new CsvReader(reader);

            while (csv.Read())
            {
                if (csv.Context.Row <= startOfData)
                    continue;

                var row = csv.Context.Record;
                var occCat = OccupancyCategoryFactory.Create(row);
                occList.Add(occCat);
            }

            return occList;
        }
    }
}
