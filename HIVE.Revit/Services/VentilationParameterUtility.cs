using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;

namespace Hive.Revit.Services
{
    public class VentilationParameterUtility
    {
        private static readonly string ACHM = "ACHM";
        private static readonly string ACHR = "ACHR";
        private static readonly string OAACHM = "OAACHM";
        private static readonly string OAACHR = "OAACHR";
        private static readonly string Pressurization_Required = "REQ_PRESS";
        private static readonly string Pressurization_Model = "REQ_MODEL";
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
                    //Pressurization_Required,
                    //Pressurization_Model,
                };
            }
        }

        public static bool ModelHasVentParameters(Document doc)
        {
            return VentParameters.All(p => RevitParameterUtility.ModelHasParameter(doc, BuiltInCategory.OST_MEPSpaces, p));
        }

        internal static IList<Parameter> CreateVentParametersInModel(Document doc)
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
            var spFile = CreateOrGetSharedParameterFile(doc); // start here again - file already has parameters in shared parameter file now

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
                var field = schedule.Definition.GetSchedulableFields()
                    .FirstOrDefault(f => f.ParameterId == parameter.Id);

                schedule.Definition.AddField(field);
            }

            catch (Exception e)
            {
                throw new Exception("Could not add parameter to schedule", e);
            }
        }

        public static void AddParameterToSchedule(ViewSchedule schedule, params Parameter[] parameters)
        {
            foreach (var p in parameters)
            {
                AddParameterToSchedule(schedule, p);
            }
        }
    }
}
