using Autodesk.Revit.DB.Mechanical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirChangeTracer
{
    public class SpacePropertyService
    {
        public void GetSpaceTypeAsString(Space sp)
        {
            var sp_type = sp.SpaceType.ToString();
        }

        // AddSomeParameter
        // FindSomeParameter
        public void GetOutsideAirChanges(Space space)
        {

        }
    }
}
