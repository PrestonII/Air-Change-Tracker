using System;

namespace Hive.Domain.Services.Ventilation
{
    public abstract class BaseVentilationCalculationService
    {
        protected const double Time = 60.0;

        public abstract double CalculateMaxACH();

    }

    public class CalculationService_ASHRAE_170 : BaseVentilationCalculationService
    {
        public double CalculateCFMBasedOnVentACH(double area, double ceilingHeight, double percentageOutsideAir)
        {
            // find ventACH based on lookup
            double ventACH = 0;

            var temp = (ventACH * area * ceilingHeight) / Time;

            double finalCFM = temp / percentageOutsideAir;

            return finalCFM; 
        }

        public double CalculateCFMBasedOnSupplyACH()
        {
            throw new NotImplementedException();
        }

        // Next, take the max between the previous two
        public override double CalculateMaxACH()
        {
            //var ventCFM = CalculateCFMBasedOnVentACH();
            //var supplyCFM = CalculateCFMBasedOnSupplyACH();

            //return ventCFM > supplyCFM ? ventCFM : supplyCFM;
            throw new NotImplementedException();
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
