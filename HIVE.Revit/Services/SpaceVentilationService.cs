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

        public static DefinitionFile CreateOrGetSharedParameterFile(Document doc)
        {
            var spFile = doc.Application.OpenSharedParameterFile();

            if (spFile != null)
            {
                spFile = RevitParameterUtility
                    .CreateOrGetSharedParameterFile(doc.Application,
                    Path.GetDirectoryName(doc.PathName).ToString() + ParameterFileName);
            }

            return spFile;
        }

        public static void AddVentParametersToSchedule(ViewSchedule schedule)
        {
            Definition d;

            //var p = schedule.Definition.GetSchedulableFields().First(f => f.ParameterId == d.)
        }

        /// <summary>
        /// Adds ventilation parameters necessary for scheduling to the model
        /// if they do not already exist
        /// </summary>
        /// <param name="doc"></param>
        public static void AddVentParametersToModel(Document doc)
        {
            var spaceCat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_MEPSpaces);
            var spFile = CreateOrGetSharedParameterFile(doc);
            var ventParams = VentilationParameterFactory.CreateOrGetVentParameters(spFile);

            foreach (var p in ventParams)
            {
                if(!RevitParameterUtility.ModelHasParameter(doc, p))
                    RevitParameterUtility.BindParameterToCategory(doc, spaceCat, p);
            }
        }


        public static ViewSchedule CreateOrGetVentilationSchedule(Document doc)
        {
            //var spFile = CreateOrGetSharedParameterFile(doc);
            //var ventParams = VentilationParameterFactory.CreateOrGetVentParameters(spFile);

            return VentilationScheduleFactory.CreateVentilationSchedule(doc);
        }

        /// <summary>
        /// Gets values of Ventilation Requirement parameters based on Space values
        /// </summary>
        /// <param name="spaces"></param>
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
