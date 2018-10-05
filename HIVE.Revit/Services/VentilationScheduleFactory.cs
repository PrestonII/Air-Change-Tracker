using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace Hive.Revit.Services
{
    public class VentilationScheduleFactory
    {
        private static string ScheduleName
        {
            get { return "Ventilation Schedule"; }
        }

        public static ViewSchedule CreateVentilationSchedule(Document doc)
        {
            return RevitScheduleFactory.Create(doc, BuiltInCategory.OST_MEPSpaces, ScheduleName);
        }
    }
}
