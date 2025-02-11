using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelJournalDataAccessLayer
{
    public class CategoryData
    {
        static public bool GetCategoryByID(int CategoryID, ref string CategoryName)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = "select * from Categories where CategoryID = @CategoryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CategoryID", CategoryID);

            bool isFound = false;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    CategoryName = (string)reader["CategoryName"];

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

        static public bool GetCategoryByName(string CategoryName, ref int CategoryID)
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = "select * from Categories where CategoryName = @CategoryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CategoryName", CategoryName);

            bool isFound = false;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    CategoryID = Convert.ToInt16(reader["CategoryID"]);

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

        static public DataTable GetAllCategories()
        {
            SqlConnection connection = new SqlConnection(clsDataParameters.ConnectionString);

            string query = "select * from Categories";

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
