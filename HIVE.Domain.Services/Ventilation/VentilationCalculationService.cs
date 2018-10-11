using HIVE.Domain.Entities;
using System;

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

        public static double CalculateModeledPressurization(Space space)
        {
            var negativePressure = Math.Abs(space.CFM_Exhaust) + Math.Abs(space.CFM_Vent);
            var pressure = space.CFM_Supply - negativePressure;

            return pressure;
        }
    }
}
