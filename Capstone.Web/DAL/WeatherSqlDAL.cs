using System;
using System.Collections.Generic;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAL : IWeatherDAL
    {
        private readonly string connectionString;

        public WeatherSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<WeatherModel> GetWeatherByPark(string parkCode)
        {
            IList<WeatherModel> weather = new List<WeatherModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM weather WHERE parkCode = @parkCode " +
                        "ORDER BY fiveDayForecastValue;";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        WeatherModel weatherSQL = new WeatherModel
                        {
                            FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]),
                            Low = Convert.ToInt32(reader["low"]),
                            High = Convert.ToInt32(reader["high"]),
                            Forecast = Convert.ToString(reader["forecast"]),
                        };

                        weather.Add(weatherSQL);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return weather;
        }
    }
}
