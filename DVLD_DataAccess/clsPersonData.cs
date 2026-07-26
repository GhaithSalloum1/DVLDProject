using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref string NationalNo, ref DateTime DateOfBirth,
            ref byte Gender, ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {



            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT * FROM People WHERE PersonID = @PersonID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PersonID", PersonID);
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader()) {

                            if (reader.Read())
                            {
                                isFound = true;
                                FirstName = reader["FirstName"].ToString();
                                SecondName = reader["SecondName"].ToString(); 
                                ThirdName = reader["ThirdName"] == DBNull.Value ? "" : reader["ThirdName"].ToString();
                                LastName = reader["LastName"].ToString();
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                NationalNo = reader["NationalNo"].ToString();
                                Gender = Convert.ToByte(reader["Gender"]);
                                Address = reader["Address"].ToString();
                                Phone = reader["Phone"].ToString();
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                                NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                                ImagePath = reader["ImagePath"] == DBNull.Value ? "" : reader["ImagePath"].ToString();
                            }
                            else
                            {
                                isFound = false;
                            }
                        
                        }
                    }
                    catch (Exception)
                    {
                        // TODO: logging here (going to implement later)
                        throw;
                    }
                }
            }

            return isFound;
        }
    
        public static bool GetPersonInfoByNationalNo(string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref int PersonID, ref DateTime DateOfBirth,
            ref byte Gender, ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT * FROM People WHERE NationalNo = @NationalNo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                PersonID = Convert.ToInt32(reader["PersonID"]);
                                FirstName = reader["FirstName"].ToString();
                                SecondName = reader["SecondName"].ToString();
                                ThirdName = reader["ThirdName"] == DBNull.Value ? "" : reader["ThirdName"].ToString();
                                LastName = reader["LastName"].ToString();
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                Gender = Convert.ToByte(reader["Gender"]);
                                Address = reader["Address"].ToString();
                                Phone = reader["Phone"].ToString();
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                                NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                                ImagePath = reader["ImagePath"] == DBNull.Value ? "" : reader["ImagePath"].ToString();
                            }
                            else
                            {
                                isFound = false;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // TODO: logging here (going to implement later)
                        throw;
                    }
                }
            }
            return isFound;
        }

        public static int AddNewPerson(string FirstName, string SecondName,
            string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
            byte Gender, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {

            int personID = -1;

            string query = @"INSERT INTO People (FirstName, SecondName, ThirdName,LastName,NationalNo,
                                                   DateOfBirth,Gender,Address,Phone, Email, NationalityCountryID,ImagePath)
                             VALUES (@FirstName, @SecondName,@ThirdName, @LastName, @NationalNo,
                                     @DateOfBirth,@Gender,@Address,@Phone, @Email,@NationalityCountryID,@ImagePath);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@ThirdName", string.IsNullOrWhiteSpace(ThirdName) ? (object)DBNull.Value : ThirdName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(Email) ? (object)DBNull.Value : Email);
                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                    command.Parameters.AddWithValue("@ImagePath", string.IsNullOrWhiteSpace(ImagePath) ? (object)DBNull.Value : ImagePath);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            personID = Convert.ToInt32(insertedID);
                        }
                    }
                    catch (Exception)
                    {
                        personID = -1;
                        // TODO: logging here (going to implement later)
                        throw;
                    }
                }

            }

            return personID;

        }
    
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT People.PersonID, People.NationalNo,
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  People.DateOfBirth, People.Gender,  
				  CASE
                  WHEN People.Gender = 0 THEN 'Male'

                  ELSE 'Female'

                  END as GenderCaption ,
			  People.Address, People.Phone, People.Email, 
              People.NationalityCountryID, Countries.CountryName, People.ImagePath
              FROM            People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
                ORDER BY People.FirstName";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
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
            }

            return dt;

        }
        
        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName,
            string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
            byte Gender, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            int rawsAffected = 0;

            string query = @"UPDATE People SET FirstName = @FirstName, SecondName = @SecondName, ThirdName = @ThirdName,
                             LastName = @LastName, NationalNo = @NationalNo, DateOfBirth = @DateOfBirth,
                             Gender = @Gender, Address = @Address, Phone = @Phone, Email = @Email, NationalityCountryID = @NationalityCountryID, ImagePath = @ImagePath
                             WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@ThirdName", string.IsNullOrWhiteSpace(ThirdName) ? (object)DBNull.Value : ThirdName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(Email) ? (object)DBNull.Value : Email);
                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                    command.Parameters.AddWithValue("@ImagePath", string.IsNullOrWhiteSpace(ImagePath) ? (object)DBNull.Value : ImagePath);

                    try
                    {
                        connection.Open();
                        rawsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        rawsAffected = -1;
                        // TODO: logging here (going to implement later)
                        throw;
                    }
                }
            }

                    return rawsAffected > 0;


        }

        public static bool DeletePerson(int PersonID)
        {
        
            int rawsAffected = 0;

            string query = @"DELETE FROM People WHERE PersonID = @PersonID";

            using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();
                        rawsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        // TODO: logging here (going to implement later)
                        throw;
                    }
                }
            }

            return rawsAffected > 0;
        }

        public static bool IsPersonExists(int PersonID)
        {
            bool isExists = false;

            using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT COUNT(*) FROM People WHERE PersonID = @PersonID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PersonID", PersonID);
                    try
                    {
                        connection.Open();
                        object count = cmd.ExecuteScalar();
                        if (count != null && (int)count > 0)
                        {
                            isExists = true;
                        }
                    }
                    catch (Exception)
                    {
                        // TODO: logging here (going to implement later)
                        throw;
                    }
                }
            }
            
            return isExists;
        }

        public static bool IsPersonExists(string NationalNo)
        {
            bool isExists = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT COUNT(*) FROM People WHERE NationalNo = @NationalNo";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
                    try
                    {
                        connection.Open();
                        object count = cmd.ExecuteScalar();
                        if (count != null && (int)count > 0)
                        {
                            isExists = true;
                        }
                    }
                    catch (Exception)
                    {
                        // TODO: logging here (going to implement later)
                        throw;
                    }
                }
            }

            return isExists;
        }


        //helper methods will be implemented later to reduce code duplication and improve maintainability,
        //such as methods for executing queries, handling exceptions, and mapping data from SqlDataReader to objects.

    }
}
