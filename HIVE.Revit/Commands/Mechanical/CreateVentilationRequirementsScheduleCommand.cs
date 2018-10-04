using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

namespace Hive.Revit.Commands.Mechanical
{
    // add transaction attributes
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CreateVentilationRequirementsScheduleCommand : BaseCommand
    {
        protected override Result Work()
        {
            // Create Ventilation Schedule

            // Add necessary parameters to model

            // Compare Space Types to lookup table and fill out
                // ACHR
                // OAACHR

            throw new System.NotImplementedException();
        }
    }
}
