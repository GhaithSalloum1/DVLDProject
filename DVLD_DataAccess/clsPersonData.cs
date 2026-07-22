using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    internal class clsPersonData
    {
        public static bool getPersonByID(int PersonID, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref string NationalNo, ref DateTime DateOfBirth,
            ref short Gendor, ref string Address, ref string Phone, ref string Email,
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
                                Gendor = Convert.ToByte(reader["Gendor"]);
                                Address = reader["Address"].ToString();
                                Phone = reader["Phone"].ToString();
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                                NationalityCountryID = (int)reader["NationalityCountryID"];
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
                        throw;
                    }
                }
            }

            return isFound;
        }
    
        public static bool getPersonByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
            ref short Gendor, ref string Address, ref string Phone, ref string Email,
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

                                PersonID = (int)reader["PersonID"];
                                FirstName = reader["FirstName"].ToString();
                                SecondName = reader["SecondName"].ToString();
                                ThirdName = reader["ThirdName"] == DBNull.Value ? "" : reader["ThirdName"].ToString();
                                LastName = reader["LastName"].ToString();
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                Gendor = Convert.ToByte(reader["Gendor"]);
                                Address = reader["Address"].ToString();
                                Phone = reader["Phone"].ToString();
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                                NationalityCountryID = (int)reader["NationalityCountryID"];
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
                        throw;
                    }
                }
            }
            return isFound;
        }

        
    
    
    
    }
}
