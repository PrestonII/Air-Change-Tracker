using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirChangeTracer.Services
{
    public class CalculationService_ASHRAE_170
    {
        private const double Time = 60.0;

        public double CalculateVentACH(double area, double ceilingHeight, double percentageOutsideAir)
        {
            // find ventACH based on lookup
            double ventACH = 0;

            var temp = (ventACH * area * ceilingHeight) / Time;

            double finalCFM = temp / percentageOutsideAir;

            return finalCFM; throw new NotImplementedException();
        }

        public double CalculateSupplyACH()
        {
            throw new NotImplementedException();
        }

        // Next, take the max between the previous two
        public void FindMaxACH()
        {
            // return greater btwn ventACH method and suppACH

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
