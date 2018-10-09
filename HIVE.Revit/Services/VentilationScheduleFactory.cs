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

        public static ViewSchedule CreateOrGetVentilationSchedule(Document doc)
        {
            var schedule = GetVentilationSchedule(doc);

            if(schedule == null)
                schedule = RevitScheduleFactory.Create(doc, BuiltInCategory.OST_MEPSpaces, ScheduleName);

            return schedule;
        }

        public static ViewSchedule GetVentilationSchedule(Document doc)
        {
            try
            {
                var schedule = new FilteredElementCollector(doc)
                    .OfType<ViewSchedule>()
                    .FirstOrDefault(s => s.Name == ScheduleName);

                return schedule;
            }

            catch (Exception e)
            {
                return null;
            }
        }
    }
}
