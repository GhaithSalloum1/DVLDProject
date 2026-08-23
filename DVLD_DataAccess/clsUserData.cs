using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
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
        
        public static int AddUser(int PersonID, string UserName, string Password, bool IsActive)
        {

            int UserID = -1;
            string query = @"INSERT INTO Users (PersonID, UserName, Password, IsActive) 
                            VALUES (@PersonID, @UserName, @Password, @IsActive);
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                try
                {
                    connection.Open();

                    object dt = command.ExecuteScalar();

                    if (dt != null && int.TryParse(dt.ToString(), out int insertedID))
                    {
                        UserID = insertedID;
                    }
                    
                }
                catch (Exception)
                {
                    // TODO: logging here (going to implement later)
                    throw;
                } 
            }

            return UserID;
        }
    
        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {

            int rowsAffected = 0;

            string query = @"UPDATE Users SET 
                                          PersonID = @PersonID, 
                                          UserName = @UserName, 
                                          Password = @Password, 
                                          IsActive = @IsActive 
                                          WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@IsActive", IsActive);

                try
                {
                    connection.Open();

                    rowsAffected = command.ExecuteNonQuery();

                }
                catch (Exception)
                {

                    throw;
                }

            }

            return rowsAffected > 0;

        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT  Users.UserID, Users.PersonID,
                            FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                             Users.UserName, Users.IsActive
                             FROM  Users INNER JOIN
                            People ON Users.PersonID = People.PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }

                    }
                }
                catch (Exception e)
                {
                    throw e;
                    // Logging will be implemented here later.
                }
            }
            return dt;
        }
    
        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;
            string query = @"DELETE FROM Users WHERE UserID = @UserID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", UserID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                    // Loggin will be implemented here Later.
                }
            }
            return rowsAffected > 0;
        }




    }
}
