using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirChangeTracer
{
    public class VentilationResolutionApp : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            throw new NotImplementedException();
        }

        public Result OnStartup(UIControlledApplication application)
        {
            throw new NotImplementedException();
        }

        // sketch of intended work
        public void Nothing()
        {
            /*
             *  check the lookup table
             * 
             *  compare the space type
             * 
             */

            // add/find parameter called AIR_CHANGE_REQS
            // assign based on lookup table
            // 
        }
    }
}
