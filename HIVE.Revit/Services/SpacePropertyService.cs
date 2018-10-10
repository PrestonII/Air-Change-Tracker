using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;

namespace Hive.Revit.Services
{
    public class SpacePropertyService
    {
        public static string GetSpaceTypeAsString(Space space)
        {
            string name = "";

            try
            {
                name = space.Document.GetElement(space.SpaceTypeId).Name;
            }
            catch (Exception e)
            {

            }

            return name;
        }

        public static double GetOutsideAirFromSpace(Space space)
        {
            double oAirPercent = 0.0;

            try
            {
                var param = space.ParametersMap.get_Item("Outside Air Percentage");

                if (param != null)
                    oAirPercent = param.AsDouble();
            }

            catch (Exception e)
            {
                Console.WriteLine($"This caused an error {e}");
            }

            return oAirPercent;
        }

        public static double CalculateCeilingHeight(Space space)
        {
            return space?.Volume <= 0 
                ? CalculateCeilingHeightByCeiling(space) 
                : CalculateCeilingHeightByVolume(space);
        }

        public static double CalculateCeilingHeightByCeiling(Space space)
        {
            var ceiling = FindCeilingInSpace(space);
            var clgLocation = (LocationPoint) ceiling.Location;
            var level = (Level) space.Document.GetElement(ceiling.LevelId);
            var levelHeight = level.Elevation;
            var height = clgLocation.Point.Z - levelHeight;

            return height;
        }

        public static double CalculateCeilingHeightByVolume(Space space)
        {
            if(space.Volume == null)
                throw new Exception();

            var height =  space.Volume / space.Area;

            return height;
        }

        public static Ceiling FindCeilingInSpace(Space space)
        {
            var ceiling = ElementLocationService.FindElementInSameSpace<Ceiling>(space);

            return ceiling;
        }
    }
}
