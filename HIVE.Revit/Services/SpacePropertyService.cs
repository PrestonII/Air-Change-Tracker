using Autodesk.Revit.DB.Mechanical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace AirChangeTracer
{
    public class SpacePropertyService
    {
        public static string GetSpaceTypeAsString(Space sp)
        {
            var name = nameof(sp.SpaceType);

            return name;
        }

        public static double CalculateCeilingHeight(Space space)
        {
            if(space.Volume == null)
                throw new Exception();

            var height =  space.Volume / space.Area;

            return height;
        }

        public static double GetOutsideAirFromSpace(Space space)
        {
            double oAirPercent = 0.0;

            try
            {
                var param = space.ParametersMap.get_Item("Outside Air Percentage");

                if (param != null)
                    oAirPercent = param.AsDouble();
            }

            catch (Exception e)
            {
                Console.WriteLine($"This caused an error {e}");
            }

            return oAirPercent;
        }
    }
}
