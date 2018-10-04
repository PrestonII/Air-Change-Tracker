using System;
using System.Diagnostics;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using HIVE.Domain.Exceptions;

namespace Hive.Revit.Commands
{
    public abstract class BaseCommand : IExternalCommand
    {
        protected Stopwatch CommandTimer = new Stopwatch();
        private Result CommandResult { get; set; }
        protected ExternalCommandData ExternalCommandData { get; set; }
        protected string MainMessage { get; set; }
        protected ElementSet ElementSet { get; set; }
        protected UIApplication CurrentApplication => ExternalCommandData.Application;
        protected Document CurrentDocument => CurrentApplication.ActiveUIDocument.Document;
        protected UIDocument UiDocument => CurrentApplication.ActiveUIDocument;

        // necessary method that Revit needs to call
        Result IExternalCommand.Execute(
            ExternalCommandData excmd,
            ref string mainmessage,
            ElementSet elemset)
        {
            MainMessage = mainmessage;
            ExternalCommandData = excmd;
            ElementSet = elemset;

            return InternalExecute();
        }

        // Internal method that allows this class to use this private fields it contains
        // without having to set them necessarily.
        protected Result InternalExecute()
        {
            try
            {
                // defined in derived classes
                StartTimer();
                CommandResult = Work();
                StopTimer();

                return CommandResult;
            }

            catch (CancellableException e)
            {
                return Result.Cancelled;
            }

            catch (Exception e)
            {
                Debug.WriteLine("Command failed because of an exception");
                TaskDialog.Show("Command Failed",
                    "There was an error behind the scenes that caused the command to fail horribly and die.");
                return Result.Failed;
            }
        }

        private void StartTimer()
        {
            if (CommandTimer.Elapsed.TotalMilliseconds > 0)
                CommandTimer.Reset();

            CommandTimer.Start();
        }

        private void StopTimer()
        {
            CommandTimer.Stop();
        }

        protected abstract Result Work();
    }
}
