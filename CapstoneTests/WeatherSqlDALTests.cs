using Capstone.Web.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace Capstone.Tests
{
    [TestClass]
    public class WeatherSqlDALTests : DatabaseTest
    {
        private IWeatherDAL _weatherDAL;

        [TestInitialize]
        public void Setup()
        {
            _weatherDAL = new WeatherSqlDAL(NpGeekDbConnectionString);
        }

        [TestClass]
        public class WeatherTests : WeatherSqlDALTests
        {
            [TestMethod]
            public void GetWeatherByParkTest()
            {
                using (var connection = new SqlConnection(NpGeekDbConnectionString))
                {
                    const string sql = 
                        @"INSERT INTO park VALUES ('CVNP', 'Cuyahoga Valley National Park', 'Ohio', 32832, 696, 125, 0, 'Woodland', 2000, 2189849, 'Of all the paths you take in life, make sure a few of them are dirt.', 'John Muir', 'Though a short distance from the urban areas of Cleveland and Akron, Cuyahoga Valley National Park seems worlds away. The park is a refuge for native plants and wildlife, and provides routes of discovery for visitors. The winding Cuyahoga River gives way to deep forests, rolling hills, and open farmlands. Walk or ride the Towpath Trail to follow the historic route of the Ohio & Erie Canal', 0, 390); 
                        INSERT INTO weather VALUES ('CVNP', 1, 38, 62, 'rain');";

                    var command = connection.CreateCommand();
                    command.CommandText = sql;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                Assert.AreEqual(1, _weatherDAL.GetWeatherByPark("CVNP").Count);
            }
        }
    }
}
