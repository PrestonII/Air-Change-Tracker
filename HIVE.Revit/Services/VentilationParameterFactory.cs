using System;
using System.Data.Common;
using Autodesk.Revit.DB;

namespace Hive.Revit.Services
{
    public class VentilationParameterFactory
    {
        public string VentilationGroupName
        {
            get { return "Ventilation"; }
        }

        public Definition CreateOrGetACHRParameter(DefinitionFile file)
        {
            var group = RevitParameterUtility.CreateOrGetGroupInSharedParameterFile(file, VentilationGroupName);
            var opts = new ExternalDefinitionCreationOptions("ACHR", ParameterType.Number);
            var achr = group.Definitions.Create(opts);

            return achr;
        }

        public void BindParameterToCategory(Document doc, Category category, Definition parameter)
        {
            var categories = doc.Application.Create.NewCategorySet();
            categories.Insert(category);
            var binding = doc.Application.Create.NewInstanceBinding(categories);
            doc.ParameterBindings.Insert(parameter, binding);

            throw new Exception("The requested binding could not be completed");
        }
    }
}
