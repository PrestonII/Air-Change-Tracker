using System;
using Autodesk.Revit.DB;
using Hive.Domain.Services.Factories;
using Element = HIVE.Domain.Entities.Element;
using RevitElement = Autodesk.Revit.DB.Element;

namespace Hive.Revit.Services
{
    public class RevitElementConversionFactory<T, T1> : BaseConversionFactory<T, T1>  where T: Element where T1 : RevitElement
    {
        public override T Create(T1 obj)
        {
            throw new NotImplementedException();
        }

        public override T Create()
        {
            throw new NotImplementedException();
        }

        public override T Create(object obj)
        {
            throw new NotImplementedException();
        }
    }
}