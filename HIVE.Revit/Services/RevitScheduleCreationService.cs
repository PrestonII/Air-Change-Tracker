using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Revit.Services
{
    public class RevitScheduleCreationService
    {
        public void Create<T>(Document doc, T elementType, string schName, params string[] parameters) where T : Element
        {
            var schedule = ViewSchedule.CreateSchedule(doc, elementType.Id);

            using (var trans = new Transaction(doc, $"Creating new {elementType.GetType().Name} schedule"))
            {


                trans.Commit();
            }
        }
    }
}
