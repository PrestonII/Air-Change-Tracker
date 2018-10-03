using System;
using HIVE.Domain.Entities;

namespace Hive.Domain.Services.Ventilation
{
    public class CalculationService_2014MC : BaseVentilationCalculationService
    {
        public CalculationService_2014MC(ILookupService service) : base(service) { }

        public double CalculateExhaustCFMByArea(Space space)
        {
            throw new NotImplementedException();
        }

        public double CalculateExhaustCFMByFixtureCount(Space space)
        {
            throw new NotImplementedException();
        }

        public override double CalculateCFMBasedOnVentACH(Space space)
        {
            throw new NotImplementedException();
        }

        public override double CalculateMaxCFMByComparison(Space space)
        {
            throw new NotImplementedException();
        }
    }
}