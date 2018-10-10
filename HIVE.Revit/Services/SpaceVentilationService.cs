using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;

namespace Hive.Revit.Services
{
    public class SpaceVentilationService
    {
        /// <summary>
        /// Gets the vent parameters necessary or creates them if they are unavailable
        /// </summary>
        /// <param name="schedule"></param>
        public static void AddVentParametersToSchedule(ViewSchedule schedule)
        {
            // check if doc has vent parameters
            // if not create them
            var hasVentParams = VentilationParameterUtility.ModelHasVentParameters(schedule.Document);

            var ventParams = hasVentParams 
                ? VentilationParameterUtility.GetVentParametersFromModel(schedule.Document) 
                : VentilationParameterUtility.CreateVentParametersInModel(schedule.Document);

            // add them to the schedule as schedulable fields
            var fields = schedule.Definition.GetSchedulableFields();
            if(ventParams.Any(p => fields.All(f => f.ParameterId != p.Id)))
                VentilationParameterUtility.AddParameterToSchedule(schedule, ventParams.ToArray());
        }

        public static ViewSchedule CreateOrGetVentilationSchedule(Document doc)
        {
            return VentilationScheduleFactory.CreateOrGetVentilationSchedule(doc);
        }

        public static void SetVentilationRequirements(Document doc)
        {
            var spaces = new FilteredElementCollector(doc)
                            .OfClass(typeof(Space))
                            .ToElements()
                            .Cast<Space>();

            ApplyVentRequirementsToSpaces(spaces);
        }

        /// <summary>
        /// Gets values of Ventilation Requirement parameters from a lookup tables based on Space values
        /// </summary>
        /// <param name="spaces"></param>
        public static void ApplyVentRequirementsToSpaces(IEnumerable<Space> spaces)
        {
            foreach (var space in spaces)
            {
                VentilationParameterUtility.AssignACHRBasedOnCategory(space);
                VentilationParameterUtility.AssignOAACHRBasedOnCategory(space);
            }
        }
    }
}
