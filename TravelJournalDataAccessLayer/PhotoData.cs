using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelJournalDataAccessLayer
{
    public class PhotoData
    {
        static public bool Find(int PhotoID, ref int TripID, ref string PhotoPath)
        {
            
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"select * from Photos
                             where PhotoID = @PhotoID";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PhotoID", PhotoID);

            bool isFound = false;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    isFound = true;

                    TripID = Convert.ToInt16(reader["TripID"]);
                    PhotoPath = (string)reader["PhotoPath"];

                }

                reader.Close();
            }

            catch (Exception ex)
            {
                //error
            }

            finally
            {
                connection.Close();
            }

            return isFound;

        }

        static public bool Find(string PhotoPath, ref int PhotoID, ref int TripID)
        {

            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"select * from Photos
                             where PhotoPath = @PhotoPath";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PhotoPath", PhotoPath);

            bool isFound = false;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    TripID = Convert.ToInt16(reader["TripID"]);
                    PhotoID = Convert.ToInt16(reader["PhotoID"]);

                }

                reader.Close();
            }

            catch (Exception ex)
            {
                //error
            }

            finally
            {
                connection.Close();
            }

            return isFound;

        }
        static public DataTable GetPhotos(int TripID)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"SELECT Photos.PhotoID, Photos.PhotoPath
                             FROM Trips INNER JOIN
                                  Photos ON Trips.TripID = Photos.TripID
                            where Trips.TripID = @TripID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TripID", TripID);

            DataTable dt = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dt.Load(reader);
   
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                //error
            }

            finally
            {
                connection.Close();
            }

            return dt;
                            

        }

        static public int AddNewPhoto(int TripID, string PhotoPath)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"INSERT INTO Photos
                             VALUES(@TripID, @PhotoPath);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TripID", TripID);
            command.Parameters.AddWithValue("@PhotoPath", PhotoPath);

           

            int PhotoID = -1;

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result!=null)
                {
                    PhotoID = Convert.ToInt16(result);

                }
            }

            catch(Exception ex)
            {
                //error
            }

            finally
            {
                connection.Close();
            }

            return PhotoID;

        }

        static public bool UpdatePhoto(int PhotoID, int TripID, string PhotoPath)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"UPDATE Photos
                             SET TripID = @TripID,
                                 PhotoPath = @PhotoPath
                             WHERE PhotoID = @PhotoID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PhotoID", PhotoID);
            command.Parameters.AddWithValue("@TripID", TripID);
            command.Parameters.AddWithValue("@PhotoPath", PhotoPath);
            


            int rowsAffected = 0;

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                //error message
            }

            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;

        }

        static public bool isExist(int PhotoID)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"select isFound=1
                             from Photos 
                             where PhotoID = @PhotoID";

            SqlCommand command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@PhotoID", PhotoID);

            object result = null;

            try
            {
                connection.Open();

                result = command.ExecuteScalar();

            }

            catch (Exception ex)
            {
                //error message
            }

            finally
            {
                connection.Close();
            }

            return result != null;
        }

        static public bool DeletePhoto(int PhotoID)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"Delete from Photos
                             where PhotoID = @PhotoID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PhotoID", PhotoID);

            int rowsAffected = 0;

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                //error message
            }

            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;

        }



    }
}
