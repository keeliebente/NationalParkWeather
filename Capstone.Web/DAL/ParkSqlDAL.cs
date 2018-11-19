using System;
using System.Collections.Generic;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Linq;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAL : IParkDAL
    {
        private readonly string connectionString;

        public ParkSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ParkModel GetPark(string parkCode)
        {
            return GetAllParks().FirstOrDefault(p => p.ParkCode == parkCode);
        }

        public IList<ParkModel> GetParks()
        {
            return GetAllParks();
        }

        private IList<ParkModel> GetAllParks()
        {
            IList<ParkModel> parks = new List<ParkModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM park ORDER BY parkName";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ParkModel parkSQL = new ParkModel
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            State = Convert.ToString(reader["state"]),
                            Acreage = Convert.ToInt32(reader["acreage"]),
                            ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                            MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]),
                            NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                            Climate = Convert.ToString(reader["climate"]),
                            YearFounded = Convert.ToInt32(reader["yearFounded"]),
                            AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                            InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                            InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                            ParkDescription = Convert.ToString(reader["parkDescription"]),
                            EntryFee = Convert.ToInt32(reader["entryFee"]),
                            NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]),
                        };

                        parks.Add(parkSQL);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return parks;
        }

        public IList<ParkModel> GetFavoriteParks()
        {
            IList<ParkModel> parks = new List<ParkModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT s.parkCode, parkName, COUNT(s.parkCode) surveyCount FROM survey_result s " +
                        "JOIN park p ON(s.parkCode = p.parkCode) GROUP BY s.parkCode, parkName " +
                        "ORDER BY COUNT(s.parkCode) DESC, parkName;";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ParkModel parkSQL = new ParkModel
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            SurveyCount = Convert.ToInt32(reader["surveyCount"])
                        };

                        parks.Add(parkSQL);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return parks;
        }
    }
}
