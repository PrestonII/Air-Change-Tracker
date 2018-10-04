using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Revit.Services
{
    public class ElementLocationService
    {
        public static IEnumerable<Element> FindElementsInSameSpace(Element element)
        {
            var doc = element.Document;
            var collected = new FilteredElementCollector(doc)
                .OfCategoryId(element.Id)
                .WherePasses(new ElementIntersectsElementFilter(element))
                .ToElements();

            return collected;
        }

        public static IEnumerable<T> FindElementsInSameSpace<T>(Element element) where T : Element
        {
            var elem = FindElementsInSameSpace(element).Cast<T>();

            return elem;
        }

        public static T FindElementInSameSpace<T>(Element element) where T: Element
        {
            return FindElementsInSameSpace<T>(element).First();
        }

        public static Element FindElementInSameSpace(Element element)
        {
            return FindElementsInSameSpace(element).First();
        }
    }
}
