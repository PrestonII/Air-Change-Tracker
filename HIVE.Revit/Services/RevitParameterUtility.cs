using System;
using System.IO;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Hive.Revit.Services
{
    public class RevitParameterUtility
    {
        public static DefinitionFile CreateOrGetSharedParameterFile(Application app, string fileName)
        {
            FileStream file = null;

            if(File.Exists(fileName))
                file = File.Create(fileName);

            file?.Close();

            return AddSharedParameterFileToModel(app, fileName);
        }

        public static DefinitionFile AddSharedParameterFileToModel(Application app, string sharedParamFile)
        {
            app.SharedParametersFilename = sharedParamFile;
            var file = app.OpenSharedParameterFile();

            return file;
        }

        public static DefinitionGroup CreateOrGetGroupInSharedParameterFile(DefinitionFile file, string groupName)
        {
            var group = file.Groups.get_Item(groupName);
            group = group ?? file.Groups.Create(groupName);

            return group;
        }

        public static void BindParameterToCategory(Document doc, Category category, Definition parameter)
        {
            var categories = doc.Application.Create.NewCategorySet();
            categories.Insert(category);
            var binding = doc.Application.Create.NewInstanceBinding(categories);
            var failed = !doc.ParameterBindings.Insert(parameter, binding);

            if(failed)
                throw new Exception("The requested binding could not be completed");
        }

        public static bool ModelHasParameter(Document doc, Definition parameter)
        {
            var binding = doc.ParameterBindings.get_Item(parameter);

            return binding != null;
        }
    }
}
