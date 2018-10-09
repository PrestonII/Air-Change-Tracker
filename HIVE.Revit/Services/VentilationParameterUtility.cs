using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;

namespace Hive.Revit.Services
{
    public class VentilationParameterUtility
    {
        public static string[] VentParameters
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
        public const string ACHM = "ACHM";
        public const string ACHR = "ACHR";
        public const string OAACHM = "OAACHM";
        public const string OAACHR = "OAACHR";
        public const string Pressurization_Required = "REQ_PRESS";
        public const string Pressurization_Model = "REQ_MODEL";

        public static bool ModelHasVentParameters(Document doc)
        {
            return VentParameters.All(p => RevitParameterUtility.ModelHasParameter(doc, BuiltInCategory.OST_MEPSpaces, p));
        }

        public static IList<Parameter> GetVentParametersFromModel(Document doc)
        {
            var space = new FilteredElementCollector(doc).OfClass(typeof(Space)).FirstElement();


        }
    }
}
