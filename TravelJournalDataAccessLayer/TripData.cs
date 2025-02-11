using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelJournalDataAccessLayer
{
    public class TripData
    {
        static public bool GetTripByID(int TripID, ref string TripName, ref DateTime StartDate, ref DateTime EndDate,
                                       ref string Location, ref int CategoryID, ref string Rating, ref string Notes,
                                       ref bool IsFavorite)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = "select * from Trips where TripID = @TripID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TripID", TripID);

            bool isFound = false;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TripName = (string)reader["TripName"];
                    StartDate = (DateTime)reader["StartDate"];
                    EndDate = (DateTime)reader["EndDate"];
                    Location = (string)reader["Location"];
                    CategoryID = Convert.ToInt16(reader["CategoryID"]);

                    if(reader["Rating"] != DBNull.Value)
                    {
                        Rating = (string)reader["Rating"];

                    }
                    else
                    {
                        Rating = null;
                    }

                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];

                    }
                    else
                    {
                        Notes = null;
                    }

                    IsFavorite = (bool)reader["IsFavorite"];

                }

                reader.Close();
            }

            catch (Exception ex)
            {
                //error message
            }

            finally
            {
                connection.Close();
            }

            return isFound;

        }

        static public bool GetTripByName(string TripName, ref int TripID, ref DateTime StartDate, ref DateTime EndDate,
                                       ref string Location, ref int CategoryID, ref string Rating, ref string Notes,
                                       ref bool IsFavorite)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = "select * from Trips where TripName = @TripName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TripName", TripName);

            bool isFound = false;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TripID = Convert.ToInt16(reader["TripID"]);
                    StartDate = (DateTime)reader["StartDate"];
                    EndDate = (DateTime)reader["EndDate"];
                    Location = (string)reader["Location"];
                    CategoryID = Convert.ToInt16(reader["CategoryID"]);

                    if (reader["Rating"] != DBNull.Value)
                    {
                        Rating = (string)reader["Rating"];

                    }
                    else
                    {
                        Rating = null;
                    }

                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];

                    }
                    else
                    {
                        Notes = null;
                    }

                    IsFavorite = (bool)reader["IsFavorite"];

                }

                reader.Close();
            }

            catch (Exception ex)
            {
                //error message
            }

            finally
            {
                connection.Close();
            }

            return isFound;

        }

        static public int AddNewTrip(ref string message, string TripName, DateTime StartDate, DateTime EndDate,
                                       string Location, int CategoryID, string Rating, string Notes)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"Insert into Trips
                             values (@TripName, @StartDate, @EndDate, @Location, @CategoryID, @Rating, @Notes, @IsFavorite);
                             SELECT SCOPE_IDENTITY();";

            bool IsFavorite = false;

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("@TripName", TripName);
            command.Parameters.AddWithValue("@StartDate", StartDate);
            command.Parameters.AddWithValue("@EndDate", EndDate);
            command.Parameters.AddWithValue("@Location", Location);
            command.Parameters.AddWithValue("@CategoryID", CategoryID);

            if(Rating != null)
            {
                command.Parameters.AddWithValue("@Rating", Rating);

            }

            else
            {
                command.Parameters.AddWithValue("@Rating", DBNull.Value);

            }

            if(Notes != null)
            {
                command.Parameters.AddWithValue("@Notes", Notes);
            }

            else
            {
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            }

            command.Parameters.AddWithValue("@IsFavorite", IsFavorite);


            int NewTripID = -1;

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result!=null)
                {
                    NewTripID = Convert.ToInt16(result);
                }

            }

            catch(Exception ex)
            {
                message = ex.Message;
            }

            finally
            {
                connection.Close();
            }

            return NewTripID;

        }


        static public bool UpdateTrip(int TripID, string TripName, DateTime StartDate, DateTime EndDate,
                                       string Location, int CategoryID, string Rating, string Notes)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"UPDATE Trips
                             SET TripName = @TripName,
                                 StartDate = @StartDate,
                                 EndDate = @EndDate,
                                 Location = @Location,
                                 CategoryID = @CategoryID,
                                 Rating = @Rating,
                                 Notes = @Notes
                             WHERE TripID = @TripID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TripID", TripID);
            command.Parameters.AddWithValue("@TripName", TripName);
            command.Parameters.AddWithValue("@StartDate", StartDate);
            command.Parameters.AddWithValue("@EndDate", EndDate);
            command.Parameters.AddWithValue("@Location", Location);
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            command.Parameters.AddWithValue("@Rating", Rating);
            command.Parameters.AddWithValue("@Notes", Notes);

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

        static public bool DeleteTrip(int TripID)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"Delete from Trips
                             where TripID = @TripID";

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("@TripID", TripID);

            int rowsAffected = 0;

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }

            catch(Exception ex)
            {
                //error message
            }

            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;

        }

        static public bool isExist(int TripID)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"select isFound=1
                             from trips 
                             where TripID = @TripID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TripID", TripID);

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

        static public bool isExist(string TripName)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"select isFound=1
                             from trips 
                             where TripName = @TripName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TripName", TripName);

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

        static public DataTable GetAllTrips()
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"select * from Trips
                             order by StartDate desc";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable dt = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }

            catch(Exception ex)
            {
                //error message
            }

            finally
            {
                connection.Close();
            }

            return dt;

        }

        static public bool SetFavorite(int TripID, byte isFavorite)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"UPDATE Trips
                             SET IsFavorite = @isFavorite
                             WHERE TripID = @TripID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TripID", TripID);
            command.Parameters.AddWithValue("@isFavorite", isFavorite);

            int rowsAffected = 0;

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }

            catch(SqlException ex)
            {
                //error message
            }

            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;

        }
        
        static public DataTable GetFavoritesTrips()
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = @"select * from Trips
                             where IsFavorite = 1
                             order by StartDate desc";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable dt = new DataTable();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }

            catch (Exception ex)
            {
                //error message
            }

            finally
            {
                connection.Close();
            }

            return dt;

        }

    }
}
