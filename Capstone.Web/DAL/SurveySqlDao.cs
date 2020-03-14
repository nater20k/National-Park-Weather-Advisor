using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveySqlDao : ISurveyDao
    {
        private string connectionString = "";

        public SurveySqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //public IList<Survey> GetSurveys()
        //{
        //    IList<Survey> surveys = new List<Survey>();

        //    string getSurveysSqlCommand = @"SELECT sr.parkCode, sr.emailAddress, sr.activityLevel, sr.surveyId, sr.state, park.parkName FROM survey_result sr
        //                                    JOIN park ON park.parkCode = survey_result.parkCode
        //                                    ORDER BY (SELECT COUNT(survey_result.parkCode)
			     //                                     FROM survey_result) ASC, park.parkName ASC";
        //    try
        //    {

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            SqlCommand cmd = new SqlCommand(getSurveysSqlCommand, conn);

        //            conn.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                //ToDo figure out storing vote count
        //                Survey survey = new Survey();
        //                survey.ParkCode = Convert.ToString(reader["sr.parkCode"]);
        //                survey.EmailAddress = Convert.ToString(reader["sr.emailAddress"]);
        //                survey.ActivityLevel = Convert.ToString(reader["sr.activityLevel"]);
        //                survey.SurveyId = Convert.ToInt32(reader["sr.surveyId"]);
        //                survey.State = Convert.ToString(reader["sr.state"]);
        //            }
        //        }
        //    } 
        //    catch
        //    {
        //        surveys = new List<Survey>();
        //    }

        //    return surveys;
        //}

        public void SubmitSurvey(Survey newSurvey)
        {
            string submitSurveySqlCommand = @"INSERT INTO survey_result (parkCode, emailAddress, activityLevel, state)
                                            VALUES (@parkCode, @emailAddress, @activityLevel, @state)
                                            SELECT SCOPE_IDENTITY()";
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(submitSurveySqlCommand, conn);
                    cmd.Parameters.AddWithValue("@parkCode", newSurvey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", newSurvey.EmailAddress);
                    cmd.Parameters.AddWithValue("@activityLevel", newSurvey.ActivityLevel);
                    cmd.Parameters.AddWithValue("@state", newSurvey.State);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
