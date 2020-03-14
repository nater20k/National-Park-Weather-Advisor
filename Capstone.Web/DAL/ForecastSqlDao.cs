using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ForecastSqlDao : IForecastDao
    {
        private string connectionString = "";

        public ForecastSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Forecast> GetForecast(string parkCode)
        {
            IList<Forecast> forecasts = new List<Forecast>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM weather WHERE parkCode = @parkCode", conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Forecast forecast = new Forecast();
                        forecast.ParkCode = Convert.ToString(reader["parkCode"]);
                        forecast.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        forecast.Low = Convert.ToInt32(reader["low"]);
                        forecast.High = Convert.ToInt32(reader["high"]);
                        forecast.ForecastType = Convert.ToString(reader["forecast"]);
                        forecasts.Add(forecast);
                    }
                }
            }
            catch (Exception ex)
            {
                forecasts = new List<Forecast>();               
            }
            return forecasts;
        }
    }
}
