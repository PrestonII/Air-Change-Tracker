﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

namespace Hive.Revit.Commands.Mechanical
{
    // add transaction attributes
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CompareModelAndCodeVentilationCommand : BaseCommand
    {
        protected override Result Work()
        {
            TaskDialog.Show("Hello World", "This is the Ventilation Resolver!");

            // this is where the work gets done
            return Result.Failed;
        }
    }
}
