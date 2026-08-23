using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    internal class clsUserData
    {
            
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName,
            ref string Password, ref bool IsActive)
        {

            bool isFound = false;

            string query = @"SELECT * FROM Users WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                PersonID = (int)reader["PersonID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                            }
                            else
                                isFound = false;
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

        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName,
        ref string Password, ref bool IsActive)
        {

            bool isFound = false;

            string query = @"SELECT * FROM Users WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            UserID = (int)reader["UserID"];
                            UserName = (string)reader["UserName"];
                            Password = (string)reader["Password"];
                            IsActive = (bool)reader["IsActive"];
                        }
                        else
                            isFound = false;
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

        public static bool GetUserInfoByUserNameAndPassword(string UserName, string Password, ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;

            string query = @"SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@Password", Password);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            UserID = (int)reader["UserID"];
                            PersonID = (int)reader["PersonID"];
                            IsActive = (bool)reader["IsActive"];
                        }
                        else
                            isFound = false;
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

    }
}
