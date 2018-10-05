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

        public static IList<Definition> CreateVentParameters(DefinitionFile file)
        {
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
