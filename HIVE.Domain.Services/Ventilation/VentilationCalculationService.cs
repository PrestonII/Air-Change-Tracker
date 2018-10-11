using HIVE.Domain.Entities;

namespace Hive.Domain.Services.Ventilation
{
    public class VentilationCalculationService
    {
        public static double CalculateCFMBasedOnSupplyACH(Space space)
        {
            return CalculationService_ASHRAE_170.CalculateCFMBasedOnSupplyACH(
                space.Area, 
                space.CeilingHeight, 
                space.OccupancyCategory);
        }

        public static double CalculateCFMBasedOnVentACH(Space space)
        {
            return CalculationService_ASHRAE_170.CalculateCFMBasedOnVentACH(
                space.Area, 
                space.CeilingHeight, 
                space.PercentageOfOutsideAir, 
                space.OccupancyCategory);
        }
    }
}
