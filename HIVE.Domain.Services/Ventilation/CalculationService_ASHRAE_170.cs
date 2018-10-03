using System;
using HIVE.Domain.Entities;

namespace Hive.Domain.Services.Ventilation
{
    public class CalculationService_ASHRAE_170 : BaseVentilationCalculationService
    {
        public CalculationService_ASHRAE_170(ILookupService service) : base(service) { }

        private double CalculateCFMBasedOnVentACH(double area, double ceilingHeight, double percentageOutsideAir, string category)
        {
            // find ventACH based on lookup
            double ventACH = _lookupService.GetVentACHBasedOnOccupancyCategory(category);

            var temp = (ventACH * area * ceilingHeight) / Time;

            double finalCFM = temp / percentageOutsideAir;

            return finalCFM; 
        }

        private double CalculateCFMBasedOnSupplyACH(double area, double ceilingHeight, string category)
        {
            var supplyACH = _lookupService.GetSupplyACHBasedOnOccupancyCategory(category);

            var cfm = (supplyACH * area * ceilingHeight) / Time;

            return cfm;
        }

        public double CalculateCFMBasedOnSupplyACH(Space space)
        {
            return CalculateCFMBasedOnSupplyACH(space.Area, space.CeilingHeight, space.OccupancyCategory);
        }

        public override double CalculateCFMBasedOnVentACH(Space space)
        {
            return CalculateCFMBasedOnVentACH(space.Area, space.CeilingHeight, space.PercentageOfOutsideAir, space.OccupancyCategory);
        }

        // Next, take the max between the previous two
        public override double CalculateMaxCFMByComparison(Space space)
        {
            var ventCFM = CalculateCFMBasedOnVentACH(space);
            var supplyCFM = CalculateCFMBasedOnSupplyACH(space);

            return ventCFM > supplyCFM ? ventCFM : supplyCFM;
        }
    }
}