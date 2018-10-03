using System;
using HIVE.Domain.Entities;

namespace Hive.Domain.Services.Ventilation
{
    public abstract class BaseVentilationCalculationService
    {
        protected const double Time = 60.0;
        protected ILookupService _lookupService;

        protected BaseVentilationCalculationService(ILookupService service)
        {
            _lookupService = service;
        }

        public abstract double CalculateMaxACH(Space space);
        public abstract double CalculateCFMBasedOnVentACH(Space space);
    }

    public class CalculationService_ASHRAE_170 : BaseVentilationCalculationService
    {
        public CalculationService_ASHRAE_170(ILookupService service) : base(service) { }

        public double CalculateCFMBasedOnVentACH(double area, double ceilingHeight, double percentageOutsideAir, string category)
        {
            // find ventACH based on lookup
            double ventACH = _lookupService.GetVentACHBasedOnOccupancyCategory(category);

            var temp = (ventACH * area * ceilingHeight) / Time;

            double finalCFM = temp / percentageOutsideAir;

            return finalCFM; 
        }

        public double CalculateCFMBasedOnSupplyACH(Space space)
        {
            throw new NotImplementedException();
        }

        // Next, take the max between the previous two
        public override double CalculateMaxACH(Space space)
        {
            var ventCFM = CalculateCFMBasedOnVentACH(space);
            var supplyCFM = CalculateCFMBasedOnSupplyACH(space);

            return ventCFM > supplyCFM ? ventCFM : supplyCFM;
        }

        public override double CalculateCFMBasedOnVentACH(Space space)
        {
            return CalculateCFMBasedOnVentACH(space.Area, space.CeilingHeight, space.PercentageOfOutsideAir, space.OccupancyCategory);
        }
    }

    public class CalculationService_2014MC
    {
        public double CalculateVentACH()
        {
            throw new NotImplementedException();
        }

        public double CalculateExhaustACHByArea()
        {
            throw new NotImplementedException();
        }

        public double CalculateExhaustACHByFixtureCount()
        {
            throw new NotImplementedException();
        }
    }

    public class CalculationService_ASHRAE_621
    {
        public double CalculateVentACH()
        {
            throw new NotImplementedException();
        }

        public double CalculateExhaustACH()
        {
            throw new NotImplementedException();
        }
    }
}
