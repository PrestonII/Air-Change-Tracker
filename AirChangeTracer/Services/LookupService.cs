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

                var occCat = csv.GetField(0);
                var iprp = csv.GetField(1);
                var ipra = csv.GetField(2);
                var sirp = csv.GetField(3);
                var sira = csv.GetField(3);
                var defaultDens = csv.GetField(4);
                var exhReqs2013 = csv.GetField(5);
                //var blank0 = csv.GetField(6);
                // 2014 mech code reqs
                var outdoorAir = csv.GetField(7);
                var maxOccDen = csv.GetField(8);
                var exhReqs2014 = csv.GetField(9);
                // ashrae 170
                //var blank0 = csv.GetField(10)
                var ventACPH = csv.GetField(11);

                thing = new OccupancyLookup
                {
                    OccupancyCategory = occCat,
                    OutdoorAirRates = new OutdoorAirRates
                    {
                        IP = new IP
                        {
                            Rp = iprp,
                            Ra = ipra
                        },
                        SI = new SI
                        {
                            Rp = sirp,
                            Ra = sira,
                        },
                        DefaultOccupancyDensity = 
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
