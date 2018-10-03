using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirChangeTracer.Services
{
    public class LookupService
    {
        public void ReadCSVToList(string path)
        {
            OccupancyLookup thing = null;
            var reader = File.OpenText(path);

            var csv = new CsvReader(reader);

            //csv.Read();
            //csv.ReadHeader();

            while (csv.Read())
            {
                if (csv.Context.Row <= 4)
                    continue;

                var value1 = csv.GetField(0);
                var value2 = csv.GetField(1);
                var value3 = csv.GetField(2);
                var value4 = csv.GetField(3);

                thing = new OccupancyLookup
                {
                    OccupancyCategory = value1,
                    OutdoorAirRates = new OutdoorAirRates
                    {
                        IP = new IP
                        {
                            Rp = value2,
                            Ra = value3
                        }
                    }
                };

                Console.WriteLine(thing);
            }
        }

        public class OccupancyLookup
        {
            public string OccupancyCategory { get; set; }
            public OutdoorAirRates OutdoorAirRates { get; set; }
        }

        public class OutdoorAirRates
        {
            public IP IP { get; set; }
            public SI SI { get; set; }
            public string DefaultOccupancyDensity { get; set; }
            public string ExhaustRequirements { get; set; }
        }

        public class IP
        {
            public string Rp { get; set; }
            public string Ra { get; set; }
        }
        public class SI
        {
            public string Rp { get; set; }
            public string Ra { get; set; }
        }
    }
}
