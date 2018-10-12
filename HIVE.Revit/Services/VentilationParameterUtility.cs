using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Hive.Domain.Services.Ventilation;
using Hive.Revit.Factory;

namespace Hive.Revit.Services
{
    public class VentilationParameterUtility
    {
        private static readonly string ACHM = "ACHM";
        private static readonly string ACHR = "ACHR";
        private static readonly string OAACHM = "OAACHM";
        private static readonly string OAACHR = "OAACHR";
        private static readonly string Pressurization_Required = "PRESSURIZATION_REQ";
        private static readonly string Pressurization_Model = "PRESSURIZATION_MOD";
        private const string ParameterFileName = "VentParameters.txt";
        private static readonly string ParameterGroup = "Ventilation";
        private static string[] VentParameters
        {
            get
            {
                return new[]
                {
                    ACHM,
                    ACHR,
                    OAACHM,
                    OAACHR,
                    Pressurization_Required,
                    Pressurization_Model,
                };
            }
        }

        public static bool ModelHasVentParameters(Document doc)
        {
            return VentParameters.All(p => RevitParameterUtility.ModelHasParameter(doc, BuiltInCategory.OST_MEPSpaces, p));
        }

        public static IList<Parameter> CreateVentParametersInModel(Document doc)
        {
            AddVentParametersToModel(doc);

            return GetVentParametersFromModel(doc);
        }

        public static IList<Parameter> GetVentParametersFromModel(Document doc)
        {
            var ventParams = VentParameters.Select(p => RevitParameterUtility.GetParameterFromCategory(doc, BuiltInCategory.OST_MEPSpaces, p)).ToList();

            return ventParams;
        }

        /// <summary>
        /// Adds ventilation parameters necessary for scheduling to the model
        /// if they do not already exist
        /// </summary>
        /// <param name="doc"></param>
        public static void AddVentParametersToModel(Document doc)
        {
            var spaceCat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_MEPSpaces);
            var buildLocation = Path.GetDirectoryName(typeof(VentilationParameterUtility).Assembly.Location);
            var dirname = Path.Combine(buildLocation, "Data");
            var paramFile = Path.Combine(dirname, ParameterFileName);
            var spFile = RevitParameterUtility.AddSharedParameterFileToModel(doc.Application, paramFile);
            var ventParams = VentilationParameterFactory.GetVentParameterDefinitions(doc);

            foreach (var p in ventParams)
            {
                if (!RevitParameterUtility.ModelHasParameter(doc, p))
                    RevitParameterUtility.BindParameterToCategory(doc, spaceCat, p);
            }
        }

        public static DefinitionFile CreateOrGetSharedParameterFile(Document doc)
        {
            var spFile = doc.Application.OpenSharedParameterFile();

            if (spFile == null)
            {
                var buildLocation = Path.GetDirectoryName(typeof(VentilationParameterUtility).Assembly.Location);
                var dirname = Path.Combine(buildLocation, "Data");
                var paramFile = Path.Combine(dirname, ParameterFileName);

                spFile = RevitParameterUtility.CreateOrGetSharedParameterFile(doc.Application, paramFile);
            }

            return spFile;
        }

        public static void AddParameterToSchedule(ViewSchedule schedule, Parameter parameter)
        {
            try
            {
                using (var tr = new Transaction(schedule.Document))
                {
                    if (!tr.HasStarted())
                        tr.Start("Adding parameter to schedule");

                    var field = schedule.Definition.GetSchedulableFields()
                        .FirstOrDefault(f => f.ParameterId == parameter.Id);

                    schedule.Definition.AddField(field);
                    //schedule.Document.Regenerate();

                    tr.Commit();
                }
            }

            catch (Exception e)
            {
                //throw new Exception("Could not add parameter to schedule", e);
            }
        }

        public static void AddParameterToSchedule(ViewSchedule schedule, params Parameter[] parameters)
        {
            foreach (var p in parameters)
            {
                if(!RevitParameterUtility.ScheduleHasParameter(schedule, p))
                    AddParameterToSchedule(schedule, p);
            }
        }

        /// <summary>
        /// always SUPPLY
        /// </summary>
        /// <param name="space"></param>
        /// <returns></returns>
        public static void AssignACHRBasedOnCategory(Space space)
        {
            var spaceType = SpacePropertyService.GetSpaceTypeAsString(space);
            var achr = VentilationLookupService.GetACHRBasedOnOccupancyCategory(spaceType);
            RevitParameterUtility.SetParameterValue(space, "ACHR", achr.ToString());
        }

        /// <summary>
        /// Always VENT
        /// </summary>
        /// <param name="space"></param>
        public static void AssignOAACHRBasedOnCategory(Space space)
        {
            var spaceType = SpacePropertyService.GetSpaceTypeAsString(space);
            var oaachr = VentilationLookupService.GetOAACHRBasedOnOccupancyCategory(spaceType);
            RevitParameterUtility.SetParameterValue(space, "OAACHR", oaachr.ToString());
        }

        public static void AssignACHMBasedOnCategory(Space space)
        {
            var factory = new SpaceConversionFactory();
            var spaceType = SpacePropertyService.GetSpaceTypeAsString(space);
            var dSpace = factory.Create(space);
            var achm = VentilationCalculationService.CalculateCFMBasedOnSupplyACH(dSpace);
            RevitParameterUtility.SetParameterValue(space, "ACHM", achm.ToString());
        }

        public static void AssignOAACHMBasedOnCategory(Space space)
        {
            var factory = new SpaceConversionFactory();
            var spaceType = SpacePropertyService.GetSpaceTypeAsString(space);
            var dSpace = factory.Create(space);
            var oaachm = VentilationCalculationService.CalculateCFMBasedOnSupplyACH(dSpace);
            RevitParameterUtility.SetParameterValue(space, "OAACHM", oaachm.ToString());
        }

        public static void AssignRequiredPressurization(Space space)
        {
            var spaceType = SpacePropertyService.GetSpaceTypeAsString(space);
            var press = VentilationLookupService.GetRequirePressurizationBasedOnOccupancy(spaceType);
            RevitParameterUtility.SetParameterValue(space, "PRESSURIZATION_REQ", press.ToString());
        }

        public static void AssignModeledPressurization(Space space)
        {
            var factory = new SpaceConversionFactory();
            var spaceType = SpacePropertyService.GetSpaceTypeAsString(space);
            var dSpace = factory.Create(space);
            var press = VentilationCalculationService.CalculateModeledPressurization(dSpace);
            RevitParameterUtility.SetParameterValue(space, "PRESSURIZATION_MOD", press.ToString());
        }
    }
}
