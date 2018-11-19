using Capstone.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class WeatherModelTests
    {
        WeatherModel weatherModel;

        [TestInitialize]
        public void Initialize()
        {
            weatherModel = new WeatherModel();
        }

        [TestMethod]
        public void GetDayOfWeekTest()
        {
            weatherModel.FiveDayForecastValue = 1;
            Assert.AreEqual("Today", weatherModel.GetDayOfWeek());
        }

        [TestMethod]
        public void CelsiusLowTest()
        {
            weatherModel.Low = 50;
            Assert.AreEqual(10, weatherModel.CelsiusLow());
        }

        [TestMethod]
        public void CelsiusHighTest()
        {
            weatherModel.High = 70;
            Assert.AreEqual(21, weatherModel.CelsiusHigh());
        }

        [TestMethod]
        public void RecommendationTest()
        {
            weatherModel.Low = 10;
            weatherModel.High = 40;
            weatherModel.Forecast = "snow";
            Assert.AreEqual("Exposure to frigid temperatures is dangerous!", weatherModel.Recommendation()[0]);
            Assert.AreEqual("Wear breathable layers", weatherModel.Recommendation()[1]);
            Assert.AreEqual("Pack snowshoes", weatherModel.Recommendation()[2]);

            weatherModel.Low = 70;
            weatherModel.High = 80;
            weatherModel.Forecast = "sunny";
            Assert.AreEqual("Bring an extra gallon of water", weatherModel.Recommendation()[0]);
            Assert.AreEqual("Pack sunblock", weatherModel.Recommendation()[1]);
        }
    }
}
