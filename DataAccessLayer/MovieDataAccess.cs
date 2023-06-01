using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace MovieTheater
{
    public class MovieDataAccess : BaseTier
    {
        public MovieDataAccess() : base()
        {

        }

        public Movies MovieVariablesSet(Movies movie)
        {
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = movie.Name;
            cmd.Parameters.Add("@date",SqlDbType.DateTime).Value = movie.Date;
            cmd.Parameters.Add("@agerating", SqlDbType.NVarChar, 50).Value = movie.AgeRating;
            cmd.Parameters.Add("@runtime", SqlDbType.Int).Value = movie.RunTime;
            cmd.Parameters.Add("@category", SqlDbType.NVarChar, 50).Value = movie.Category;
            cmd.Parameters.Add("@description", SqlDbType.NVarChar, 50).Value = movie.Description;
            cmd.Parameters.Add("@language", SqlDbType.NVarChar, 50).Value = movie.Language;
            cmd.Parameters.Add("@directorid", SqlDbType.Int).Value = movie.DirectorID;
            if(movie.MovieIMG != null)
            {
                cmd.Parameters.Add("@movieimg", SqlDbType.Image).Value = movie.MovieIMG;
            }
            

            return movie;

        }

        public List<Movies> getAllMovies()
        {
            Movies movie = null;
            List<Movies> movieslist = null;
            query = "Select * from movies;";
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
                    movieslist = new List<Movies>();
                    while (reader.Read())
                    {
                        movie = new Movies();
                        movie.MovieID = (int)reader["MovieID"];
                        movie.Name = reader["Name"].ToString();
                        movie.Date = (DateTime)reader["Date"];
                        movie.AgeRating = reader["AgeRating"].ToString();
                        movie.RunTime = (int)reader["RunTime"];
                        movie.Category = reader["Category"].ToString();
                        movie.Description = reader["Description"].ToString();
                        movie.Language = reader["Language"].ToString();
                        movie.DirectorID = (int)reader["DirectorID"];

                        movieslist.Add(movie);
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

            return movieslist;

        } // ends getAllMovies
        public Movies FindMovie(int id)
        {
            Movies movie = null;
            query = "select * from movies where MovieID = @id;";
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
                    movie = new Movies();
                    movie = new Movies();
                    movie.MovieID = (int)reader["MovieID"];
                    movie.Name = reader["Name"].ToString();
                    movie.Date = (DateTime)reader["Date"];
                    movie.AgeRating = reader["AgeRating"].ToString();
                    movie.RunTime = (int)reader["RunTime"];
                    movie.Category = reader["Category"].ToString();
                    movie.Description = reader["Description"].ToString();
                    movie.Language = reader["Language"].ToString();
                    movie.DirectorID = (int)reader["DirectorID"];
                    if ( reader["MovieIMG"] != DBNull.Value )
                    {
                        movie.MovieIMG = (byte[])reader["MovieIMG"];
                    }
                    else
                    {
                        movie.MovieIMG = null;
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
            return movie;
        } // ends EditMovie


        public bool EditMovie(Movies movie)
        {
            if( movie.MovieIMG == null)
            {
                query = "Update Movies " +
                "Set Name = @name, Date = @date, AgeRating = @agerating, Runtime = @runtime , Category = @category, Description = @description" +
                ",Language = @language , DirectorID = @directorid where MovieID = @id  ;";
            }
            else
            {
                query = "Update Movies " +
                "Set Name = @name, Date = @date, AgeRating = @agerating, Runtime = @runtime , Category = @category, Description = @description" +
                ",Language = @language , DirectorID = @directorid , MovieIMG = @movieimg where MovieID = @id  ;";
            }
            int rows = 0;
            
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = movie.MovieID;

            MovieVariablesSet(movie);
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
        }// ends EditMovie


        public bool DeleteMovie(int id)
        {
            int rows = 0;
            query = "Delete from Movies where MovieID = @id;";

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

        } // ends Delete Movie

        public bool AddMovie(Movies movie)
        {
            int rows = 0;
            if (movie.MovieIMG != null)
            {
                query = "INSERT INTO Movies (Name, Date, AgeRating, Runtime, Category, Description, Language, DirectorID,MovieIMG) " +
                "VALUES (@name, @date, @agerating, @runtime, @category, @description, @language, @directorid,@movieimg);";
            }
            else
            {
                query = "INSERT INTO Movies (Name, Date, AgeRating, Runtime, Category, Description, Language, DirectorID) " +
               "VALUES (@name, @date, @agerating, @runtime, @category, @description, @language, @directorid);";
            }

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            MovieVariablesSet(movie);



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

        } // ends AddMovie


        public byte[] GetImage(int id)
        {
            query = "Select MovieIMG from Movies where MovieID = @id;";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            byte[] img;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    img = (byte[])reader["MovieIMG"];

                }
                else
                {
                    img = null;
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

            return img;
        }


    } // ends get img



}