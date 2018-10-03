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
        public string TypicalDataLocation { get; private set; }

        public VentilationLookupService()
        {
            Initialize();
        }

        private void Initialize()
        {
            ReadDataFile();
        }

        public void ReadDataFile()
        {
            var buildLocation = Path.GetDirectoryName(GetType().Assembly.Location);
            var dirname = Path.Combine(buildLocation, "Data");
            TypicalDataLocation = Path.Combine(dirname, "Lookup_VentilationTable.csv");
        }

        public List<OccupancyLookup> ReadCSVToList(string path = "", int startOfData = 0)
        {
            List<OccupancyLookup> occList = new List<OccupancyLookup>();
            path = path == "" ? TypicalDataLocation : path;

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
