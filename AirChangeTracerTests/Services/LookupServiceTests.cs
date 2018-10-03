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
        public void ReadCSVToListTest()
        {
            _service = new VentilationLookupService();
            _service.ReadCSVToList(_lookupFilePath, 4);
        }
    }
}