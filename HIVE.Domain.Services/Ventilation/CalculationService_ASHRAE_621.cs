﻿using System;
using HIVE.Domain.Entities;

namespace Hive.Domain.Services.Ventilation
{
    public class CalculationService_ASHRAE_621 : BaseVentilationCalculationService
    {
        public CalculationService_ASHRAE_621(ILookupService service) : base(service) { }

        public double CalculateCFMBasedOnExhaustACH()
        {
            throw new NotImplementedException();
        }

        public double CalculateCFMBasedOnVentACH(Space space)
        {
            throw new NotImplementedException();
        }

        //public override double CalculateMaxCFMByComparison(Space space)
        //{
        //    throw new NotImplementedException();
        //}
    }
}