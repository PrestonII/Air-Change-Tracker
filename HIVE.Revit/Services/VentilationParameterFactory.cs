using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Autodesk.Revit.DB;

namespace Hive.Revit.Services
{
    public class VentilationParameterFactory
    {
        public static string VentilationGroupName
        {
            get { return "Ventilation"; }
        }

        public static IList<Parameter> GetVentParameters(Document doc)
        {
            if (VentilationParameterUtility.ModelHasVentParameters(doc))
                return VentilationParameterUtility.GetVentParametersFromModel(doc);

            var defs = GetVentParameterDefinitions(doc);
            var pars = new List<Parameter>();

            foreach (var d in defs)
            {
                var p = RevitParameterUtility.GetParameterFromCategory(doc, BuiltInCategory.OST_MEPSpaces, d.Name);
                pars.Add(p);
            }

            return pars;
        }

        /// <summary>
        /// Gets vent parameter definitions or creates them if they don't exist
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IList<Definition> GetVentParameterDefinitions(Document doc)
        {
            var file = doc.Application.OpenSharedParameterFile();
            var list = new List<Definition>();

            var achr = CreateOrGetACHRParameter(file);
            var achm = CreateOrGetACHMParameter(file);
            var oaachr = CreateOrGetOAACHRParameter(file);
            var oaachm = CreateOrGetOAACHMParameter(file);
            // add Pressurization Required parameter (calculated formula parameter)
            // add Pressurization Current parameter (calculated formula parameter)

            var vents = new Definition[]
            {
                achr, achm, oaachr, oaachm
            };

            list.AddRange(vents);

            return list;
        }

        public static Definition CreateOrGetACHRParameter(DefinitionFile file)
        {
            var group = RevitParameterUtility.CreateOrGetGroupInSharedParameterFile(file, VentilationGroupName);
            var opts = new ExternalDefinitionCreationOptions("ACHR", ParameterType.Number);
            var achr = group.Definitions.Create(opts);

            return achr;
        }

        public static Definition CreateOrGetACHMParameter(DefinitionFile file)
        {
            var group = RevitParameterUtility.CreateOrGetGroupInSharedParameterFile(file, VentilationGroupName);
            var opts = new ExternalDefinitionCreationOptions("ACHM", ParameterType.Number);
            var achr = group.Definitions.Create(opts);

            return achr;
        }

        public static Definition CreateOrGetOAACHRParameter(DefinitionFile file)
        {
            var group = RevitParameterUtility.CreateOrGetGroupInSharedParameterFile(file, VentilationGroupName);
            var opts = new ExternalDefinitionCreationOptions("OAACHR", ParameterType.Number);
            var achr = group.Definitions.Create(opts);

            return achr;
        }

        public static Definition CreateOrGetOAACHMParameter(DefinitionFile file)
        {
            var group = RevitParameterUtility.CreateOrGetGroupInSharedParameterFile(file, VentilationGroupName);
            var opts = new ExternalDefinitionCreationOptions("OAACHM", ParameterType.Number);
            var achr = group.Definitions.Create(opts);

            return achr;
        }
    }
}
