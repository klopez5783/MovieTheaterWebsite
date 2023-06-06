using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace MovieTheater
{
    public class DirectorDataAccess : BaseTier
    {

        public DirectorDataAccess() : base()
        {

        }

        public Director directorvariablesset(Director director)
        {
            cmd.Parameters.Add("@directorid", SqlDbType.Int).Value = director.DirectorID;
            cmd.Parameters.Add("@firstname", SqlDbType.NVarChar, 50).Value = director.FirstName;
            cmd.Parameters.Add("@lastname", SqlDbType.NVarChar, 50).Value = director.LastName;

            return director;

        }

        public List<Director> getAllDirectors()
        {
            Director director = null;
            List<Director> list = null;
            query = "Select * from Directors;";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            try
            {
                // opens connection to database
                conn.Open();
                // executes the query and gets data, then puts in into the reader
                reader = cmd.ExecuteReader();
                // checks if data was returned
                if (reader.HasRows)
                {
                    list = new List<Director>();
                    while (reader.Read())
                    {
                        director = new Director();
                        director.DirectorID = (int)reader["DirectorID"];
                        director.FirstName = reader["FirstName"].ToString();
                        director.LastName = reader["LastName"].ToString();


                        list.Add(director);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return list;

        } // ends getDirectors
        public Director FindDirector(int id)
        {
            Director director = null;
            query = "select * from Directors where DirectorID = @id";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    director = new Director();
                    director.DirectorID = (int)reader["DirectorID"];
                    director.FirstName = reader["FirstName"].ToString();
                    director.LastName = reader["LastName"].ToString();

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return director;
        } // ends FindDirector


        public bool EditDirector(Director director)
        {
            query = "Update Directors" +
                " set FirstName = @firstname , LastName = @lastname where DirectorID = @directorid";
            int rows = 0;

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            directorvariablesset(director);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                conn.Close();

            }

            return success;
        }// ends EditDirector


        public bool DeleteDirector(int id)
        {
            int rows = 0;
            query = "Delete from Directors where DirectorID = @id;";

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                conn.Close();

            }

            return success;

        } // ends Delete Director

        public bool AddDirector(Director director)
        {
            int rows = 0;
            query = "Insert into Directors (FirstName,LastName) values (@firstname,@lastname);";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            directorvariablesset(director);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                conn.Close();

            }
            return success;

        } // ends Add Director





    }
}