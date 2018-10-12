using Hive.Domain.Services.Factories;
using Element = HIVE.Domain.Entities.Element;
using RevitElement = Autodesk.Revit.DB.Element;

namespace Hive.Revit.Factory
{
    public abstract class RevitConversionFactory<T, T1> : BaseConversionFactory<T, T1>  where T: Element where T1 : RevitElement {  }
}