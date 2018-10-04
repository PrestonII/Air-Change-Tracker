using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIVE.Domain.Entities;
using RevitSpace = Autodesk.Revit.DB.Mechanical.Space;

namespace Hive.Revit.Services
{
    public class SpaceConversionFactory : RevitElementConversionFactory<Space, RevitSpace >
    {
        public override Space Create(RevitSpace rSpace)
        {
            throw new NotImplementedException();
        }

        public override Space Create()
        {
            throw new NotImplementedException();
        }

        public override Space Create(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
