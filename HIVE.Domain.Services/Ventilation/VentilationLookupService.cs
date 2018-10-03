using CsvHelper;
using HIVE.Domain.Entities.Ventilation;
using System.Collections.Generic;
using System.IO;

namespace Hive.Domain.Services.Ventilation
{
    public class VentilationLookupService : ILookupService
    {
        public string TypicalDataLocation { get; private set; }
        public Dictionary<string, OccupancyLookup> DefaultDatabase { get; private set; }

        public VentilationLookupService()
        {
            Initialize();
        }

        private void Initialize()
        {
            ReadDataFile();
            SeedDefaultDatabase();
        }

        public void ReadDataFile()
        {
            var buildLocation = Path.GetDirectoryName(GetType().Assembly.Location);
            var dirname = Path.Combine(buildLocation, "Data");
            TypicalDataLocation = Path.Combine(dirname, "Lookup_VentilationTable.csv");
        }

        private void SeedDefaultDatabase()
        {
            var seed = ReadCSVToList(TypicalDataLocation, 4);
            var dict = new Dictionary<string, OccupancyLookup>();
            seed.ForEach(s => dict.Add(s.OccupancyCategory, s));
            DefaultDatabase = dict;
        }

        public double GetVentACHBasedOnOccupancyCategory(string category)
        {
            var lCat = DefaultDatabase[category];
            return lCat.MechCodeAshrae.VentilationAirChangesPerHour ?? 0.0;
        }

        public double GetSupplyACHBasedOnOccupancyCategory(string category)
        {
            var lCat = DefaultDatabase[category];
            return lCat.MechCodeAshrae.SupplyAirChangesPerHour ?? 0.0;
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
