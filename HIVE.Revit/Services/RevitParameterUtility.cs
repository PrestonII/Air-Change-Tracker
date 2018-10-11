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

            if(!File.Exists(fileName))
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
            DefinitionGroup group = null;

            try
            {
                group = file.Groups.get_Item(groupName);
            }

            catch(Exception e) { }

            group = group ?? file.Groups.Create(groupName);

            return group;
        }

        public static void BindParameterToCategory(Document doc, Category category, Definition parameter)
        {
            try
            {
                var categories = doc.Application.Create.NewCategorySet();
                categories.Insert(category);
                var binding = doc.Application.Create.NewInstanceBinding(categories);
                using (var tr = new Transaction(doc))
                {
                    if (!tr.HasStarted())
                        tr.Start("Inserting parameter");

                    doc.ParameterBindings.Insert(parameter, binding);

                    tr.Commit();
                }
            }

            catch (Exception e)
            {
                throw new Exception("The requested binding could not be completed", e);
            }

        }

        public static bool ModelHasParameter(Document doc, Definition parameter)
        {
            var binding = doc.ParameterBindings.get_Item(parameter);

            return binding != null;
        }

        public static bool ModelHasParameter(Document doc, BuiltInCategory category, string parameterName)
        {
            var par = GetParameterFromCategory(doc, category, parameterName);

            return par != null;
        }

        public static bool ModelHasParameter(Element element, string parameterName)
        {
            return GetParameterFromElement(element, parameterName) != null;
        }

        public static Parameter GetParameterFromCategory(Document doc, BuiltInCategory category, string name)
        {
            try
            {
                var elem = new FilteredElementCollector(doc).OfCategory(category).FirstElement();

                return GetParameterFromElement(elem, name);
            }

            catch(Exception e)
            {
                return null;
            }
        }

        public static Parameter GetParameterFromElement(Element element, string parameterName)
        {
            try
            {
                return element.ParametersMap.get_Item(parameterName);
            }

            catch(Exception e)
            {
                return null;
            }
        }

        public static void SetParameterValue(Element elem, string name, string val)
        {
            var doc = elem.Document;

            using (var tr = new Transaction(doc))
            {
                if (!tr.HasStarted())
                    tr.Start("Setting parameter value");

                try
                {
                    var par = elem.ParametersMap.get_Item(name);

                    SetParameterBasedOnType(par, val);

                    tr.Commit();
                }

                catch (Exception e)
                {
                    tr.RollBack();
                }
            }
        }

        private static void SetParameterBasedOnType(Parameter par, string val)
        {
            var kind = par.StorageType;

            switch (kind)
            {
                case StorageType.Double:
                    var pVal = double.Parse(val);
                    par.Set(pVal);
                    break;
                case StorageType.Integer:
                    par.Set(int.Parse(val));
                    break;
                default:
                case StorageType.None:
                case StorageType.String:
                    par.Set(val);
                    break;
            }
        }

        public static bool ScheduleHasParameter(ViewSchedule schedule, Parameter parameter)
        {
            var data = schedule.GetTableData().GetSectionData(SectionType.Body);
            var count = data.NumberOfColumns;

            for (var i = 0; i < count; i++)
            {
                var id = data.GetCellParamId(i); // figure out things here
                var name = data.GetCellText(0, i);

                if (id == parameter.Id)
                    return true;
            }

            return false;
        }
    }
}
