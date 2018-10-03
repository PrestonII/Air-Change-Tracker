using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirChangeTracer.DomainObjects;
using AirChangeTracer.DomainObjects.Ventilation;

namespace AirChangeTracer.Services
{
    public class OccupancyCategoryFactory
    {
        public static OccupancyLookup Create(string[] row)
        {
            var occCat = row[0];
            var iprp = GetDoubleFromString(row[1]);
            var ipra = GetDoubleFromString(row[2]);
            var sirp = GetDoubleFromString(row[3]);
            var sira = GetDoubleFromString(row[3]);
            var defaultDens = GetDoubleFromString(row[4]);
            var exhReqs2013 = GetDoubleFromString(row[5]);
            //var blank0 = row[6);

            // 2014 mech code reqs
            var outdoorAir = GetDoubleFromString(row[7]);
            var maxOccDen = GetDoubleFromString(row[8]);
            var exhReqs2014 = GetDoubleFromString(row[9]);
            //var blank0 = row[10)

            // ashrae 170
            var ventACPH = GetDoubleFromString(row[11]);
            var supplyACPH = GetDoubleFromString(row[12]);
            var pressure = row[13];
            var rmExh = row[14] == "YES";

            var pEnum = pressure == "NR"
                ? PressureRelationship.None
                : (pressure == "+"
                    ? PressureRelationship.Positive
                    : PressureRelationship.Negative);

            // lighting & eq table
            //var blank0 = row[15)
            var eqLoad = GetDoubleFromString(row[16]);
            var ltLoad = GetDoubleFromString(row[17]);

            var thing = new OccupancyLookup
            {
                OccupancyCategory = occCat,
                MechCode2013 = new MechanicalCode_2013
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
                    DefaultOccupancyDensity = defaultDens,
                    ExhaustRequirements = exhReqs2013
                },

                MechCode2014 = new MechanicalCode_2014
                {
                    OutdoorAir = outdoorAir,
                    MaxOccupancyDensity = maxOccDen,
                    ExhaustRequirement = exhReqs2014
                },

                MechCodeAshrae = new MechanicalCode_ASHRAE_170
                {
                    VentilationAirChangesPerHour = ventACPH,
                    SupplyAirChangesPerHour = supplyACPH,
                    PressureRelationship = pEnum,
                    AllRoomAirExhausted = rmExh
                },

                LightingEquipmentLoad = new LightingEquipmentLoad
                {
                    EquipmentLoad = eqLoad,
                    LightingLoad = ltLoad
                }
            };

            Console.WriteLine(thing);

            return thing;
        }

        public static double? GetDoubleFromString(string val)
        {
            double? finalVal = null;

            try
            {
                double.TryParse(val, out double conv);
                finalVal = conv;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return finalVal;
        }
    }
}
