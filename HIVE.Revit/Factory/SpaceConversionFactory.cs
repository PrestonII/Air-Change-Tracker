﻿using Hive.Revit.Services;
using HIVE.Domain.Entities;
using RevitSpace = Autodesk.Revit.DB.Mechanical.Space;

namespace Hive.Revit.Factory
{
    public class SpaceConversionFactory : RevitConversionFactory<Space, RevitSpace >
    {
        public override Space Create(RevitSpace rSpace)
        {
            var space = new Space
            {
                Area = rSpace.Area,
                CFM_Exhaust = rSpace.ActualExhaustAirflow,
                CFM_Supply = rSpace.ActualSupplyAirflow,
                CFM_Vent = rSpace.ActualReturnAirflow,
                NumberOfPeople = rSpace.NumberofPeople,
                OccupancyCategory = SpacePropertyService.GetSpaceTypeAsString(rSpace),
                CeilingHeight = SpacePropertyService.CalculateCeilingHeight(rSpace),
                PercentageOfOutsideAir = SpacePropertyService.GetOutsideAirFromSpace(rSpace)
            };

            return space;
        }

        public override Space Create()
        {
            return new Space();
        }

        public override Space Create(object obj)
        {
            return Create();
        }
    }
}
