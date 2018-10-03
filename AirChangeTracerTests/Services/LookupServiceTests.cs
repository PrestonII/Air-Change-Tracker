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
        private string _lookupFilePath;
        private VentilationLookupService _service;

        [SetUp]
        public void Initialize()
        {
            var buildLocation = Path.GetDirectoryName(GetType().Assembly.Location);
            var dirname = Path.Combine(buildLocation, "Resources");
            _lookupFilePath = Path.Combine(dirname, "Lookup_VentilationTable.csv");
        }

        [Test]
        public void ShouldBeAbleToReadCSVToListOfOccupancyObjects()
        {
            _service = new VentilationLookupService();
            var list = _service.ReadCSVToList(_lookupFilePath, 4);

            Assert.IsNotEmpty(list);
        }

        [Test]
        public void ShouldHave_AllIsolationAnteRoom_AsFirstCategory()
        {
            _service = new VentilationLookupService();
            var list = _service.ReadCSVToList(_lookupFilePath, 4);

            var first = list.First();
            var expected = "AII - Isolation Anteroom";
            var actual = first.OccupancyCategory;

            Assert.IsNotNull(first);
            Assert.AreEqual(expected, actual);
        }
    }
}