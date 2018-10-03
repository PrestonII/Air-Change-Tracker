using NUnit.Framework;
using AirChangeTracer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AirChangeTracer.Services.Tests
{
    [TestFixture()]
    public class LookupServiceTests
    {
        private VentilationLookupService _service;

        [SetUp]
        public void Initialize()
        {
            _service = new VentilationLookupService();
        }

        [Test]
        public void ShouldBeAbleToFindDataFile()
        {
            var path = _service.TypicalDataLocation;

            Assert.IsNotNull(path);
            Assert.IsTrue(path.Contains("Lookup_VentilationTable.csv"));
            Assert.IsTrue(File.Exists(path));
        }

        [Test]
        public void ShouldBeAbleToReadCSVToListOfOccupancyObjects()
        {
            var list = _service.ReadCSVToList(startOfData: 4);

            Assert.IsNotEmpty(list);
        }

        [Test]
        public void ShouldHave_AllIsolationAnteRoom_AsFirstCategory()
        {
            var list = _service.ReadCSVToList( startOfData: 4);

            var first = list.First();
            var expected = "AII - Isolation Anteroom";
            var actual = first.OccupancyCategory;

            Assert.IsNotNull(first);
            Assert.AreEqual(expected, actual);
        }
    }
}