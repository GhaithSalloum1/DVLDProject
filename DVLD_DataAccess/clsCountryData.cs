using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsCountryData
    {
        public static bool GetCountryInfoByID(int CountryID, ref string CountryName)
        {
            bool isFound = false;
            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CountryID", CountryID);
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader()) 
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            CountryName = reader["CountryName"].ToString();
                        }
                        else
                        {
                            isFound = false;
                            CountryName = string.Empty;
                        }
                    }
                }
                catch (Exception)
                {
                    // TODO: logging here (going to implement later)
                    throw;
                }
            }
                return isFound;
        }
        public static bool GetCountryInfoByName(string CountryName, ref int CountryID)
        {
            bool isFound = false;
            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CountryName", CountryName);
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            CountryID = Convert.ToInt32(reader["CountryID"]);
                        }
                        else
                        {
                            isFound = false;
                            CountryID = -1;
                        }
                    }
                }
                catch (Exception)
                {
                    // TODO: logging here (going to implement later)
                    throw;
                }
            }
            return isFound;
        }
        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM Countries";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception)
                {
                    // TODO: logging here (going to implement later)
                    throw;
                }
            }


            return dt;
        }

    }
}
