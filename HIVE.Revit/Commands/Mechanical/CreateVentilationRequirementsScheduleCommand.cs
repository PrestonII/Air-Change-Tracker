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
            throw new System.NotImplementedException();
        }
    }
}
