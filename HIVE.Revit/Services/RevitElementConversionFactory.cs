using System;
using Autodesk.Revit.DB;
using Hive.Domain.Services.Factories;
using Element = HIVE.Domain.Entities.Element;
using RevitElement = Autodesk.Revit.DB.Element;

namespace Hive.Revit.Services
{
    public abstract class RevitElementConversionFactory<T, T1> : BaseConversionFactory<T, T1>  where T: Element where T1 : RevitElement {  }
}