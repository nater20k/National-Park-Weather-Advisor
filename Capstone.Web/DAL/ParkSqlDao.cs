using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ParkSqlDao : IParkDao
    {
        private string connectionString = "";

        public ParkSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Park> GetParks()
        {
            IList<Park> parks = new List<Park>();

            string getParksSqlCommand = @"SELECT * FROM park";

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(getParksSqlCommand, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    parks = MapResultsToParks(reader);

                }
            }
            catch
            {
                parks = new List<Park>();
            }

            return parks;
        }

        public Park GetParkDetails(string parkCode)
        {
            IList<Park> parks = new List<Park>();

            string getParkDetailSqlCommand = @"SELECT * FROM park WHERE @parkCode = parkCode";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(getParkDetailSqlCommand, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    parks = MapResultsToParks(reader);
                }
            }
            catch
            {
                parks = new List<Park>();
            }

            return parks[0];
        }

        private IList<Park> MapResultsToParks(SqlDataReader reader)
        {
            IList<Park> parks = new List<Park>();

            while (reader.Read())
            {
                Park park = new Park();
                park.ParkCode = Convert.ToString(reader["parkCode"]);
                park.ParkName = Convert.ToString(reader["parkName"]);
                park.State = Convert.ToString(reader["state"]);
                park.Acreage = Convert.ToInt32(reader["acreage"]);
                park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                park.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
                park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                park.Climate = Convert.ToString(reader["climate"]);
                park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

                parks.Add(park);
            }

            return parks;
        }

        public IList<Park> GetBestParks()
        {
            IList<Park> parks = new List<Park>();

            string getParksSqlCommand = @"SELECT COUNT(park.parkCode) as parkCount, park.parkName, park.parkCode 
                                          FROM park
                                          JOIN survey_result
                                          ON park.parkCode = survey_result.parkCode
                                          GROUP BY park.parkCode, park.parkName
                                          ORDER BY parkCount DESC, parkName";
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(getParksSqlCommand, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = new Park();
                        park.VoteCount = Convert.ToInt32(reader["parkCount"]);
                        park.ParkName = Convert.ToString(reader["parkName"]);
                        park.ParkCode = Convert.ToString(reader["parkCode"]);
                        parks.Add(park);
                    }

                }
            }
            catch
            {
                parks = new List<Park>();
            }

            return parks;

        }
     }
}
