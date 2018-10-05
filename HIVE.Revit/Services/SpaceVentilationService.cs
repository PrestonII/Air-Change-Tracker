using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;

namespace Hive.Revit.Services
{
    public class SpaceVentilationService
    {
        private static readonly string ParameterFileName = "HiveParameters.txt";
        private static readonly string ParameterGroup = "Ventilation";

        public static void AddVentParametersToModel(Document doc)
        {
            var spaceCat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_MEPSpaces);
            var spFile = doc.Application.OpenSharedParameterFile();

            if (spFile != null)
            {
                spFile = RevitParameterUtility.CreateSharedParameterFile(doc.Application,
                    Path.GetDirectoryName(doc.PathName).ToString() + ParameterFileName);
            }

            var ventParams = VentilationParameterFactory.CreateVentParameters(spFile);

            foreach (var p in ventParams)
            {
                if(!RevitParameterUtility.ModelHasParameter(doc, p))
                    RevitParameterUtility.BindParameterToCategory(doc, spaceCat, p);
            }
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
