using System;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Hive.Revit.Services
{
    public class RevitParameterUtility
    {
        public static string CreateSharedParameterFile(string fileName)
        {
            var file = File.Create(fileName);
            file.Close();

            return fileName;
        }

        public static DefinitionFile AddSharedParameterFileToModel(UIApplication uiApp, string sharedParamFile)
        {
            var app = uiApp.Application;
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

        public void AddParameterToCategory(UIApplication uiApp, string categoryName)
        {
            var category = uiApp.ActiveUIDocument.Document.Settings.Categories.get_Item(categoryName);

        }
    }
}
