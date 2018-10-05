using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;

namespace Hive.Revit.Services
{
    public class SpaceVentilationService
    {
        public static void AddVentParametersToModel(Document doc)
        {

        }

        public static void CreateOrGetVentilationSchedule(Document doc)
        {

        }

        public static void GetVentilationRequirements(IEnumerable<Space> spaces)
        {

        }

        public static void GetVentilationRequirements(Document doc)
        {
            var spaces = new FilteredElementCollector(doc)
                            .OfClass(typeof(Space))
                            .ToElements()
                            .Cast<Space>();

            GetVentilationRequirements(spaces);
        }
    }
}
