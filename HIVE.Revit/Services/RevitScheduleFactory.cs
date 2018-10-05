using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Revit.Services
{
    public class RevitScheduleFactory
    {
        public static ViewSchedule Create(Document doc, BuiltInCategory elementType, string schName)
        {
            ViewSchedule schedule = null;

            using (var trans = new Transaction(doc, $"Creating new {elementType.GetType().Name} schedule"))
            {
                schedule = ViewSchedule.CreateSchedule(doc, new ElementId(elementType), ElementId.InvalidElementId);
                schedule.Name = schName;

                trans.Commit();
            }

            return schedule;
        }
    }
}
