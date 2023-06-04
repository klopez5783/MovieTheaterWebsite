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
    public class MovieCastDataAccess : BaseTier
    {

        public MovieCastDataAccess() : base()
        {

        }

        public MovieCast MovieCastVariablesSet(MovieCast movie)
        {
            cmd.Parameters.Add("@movieid", SqlDbType.Int).Value = movie.MovieID;
            cmd.Parameters.Add("@actorid", SqlDbType.Int).Value = movie.ActorID;
            cmd.Parameters.Add("@role", SqlDbType.NVarChar, 50).Value = movie.Role;
            
            return movie;

        }

        public List<MovieCast> getAllMoviesCast()
        {
            MovieCast movie = null;
            List<MovieCast> movieslist = null;
            query = "Select * from MovieCast;";
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
                    movieslist = new List<MovieCast>();
                    while (reader.Read())
                    {
                        movie = new MovieCast();
                        movie.MovieID = (int)reader["MovieID"];
                        movie.ActorID = (int)reader["ActorID"];
                        movie.Role = reader["Role"].ToString();
                        

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
        public MovieCast FindMovieCast(int id,int actorid)
        {
            MovieCast movie = null;
            query = "select * from MovieCast where MovieID = @id and ActorID = @actorid;";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@actorid", SqlDbType.Int).Value = actorid;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    movie = new MovieCast();
                    movie.MovieID = (int)reader["MovieID"];
                    movie.ActorID = (int)reader["ActorID"];
                    movie.Role = reader["Role"].ToString();

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


        public bool EditMovieCast(MovieCast movie)
        {
            query = "Update MovieCast" +
                " set MovieID = @movieid , ActorID = @actorid, Role = @role where MovieID = @movieid and ActorID = @actorid;";
            int rows = 0;

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            MovieCastVariablesSet(movie);
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


        public bool DeleteMovie(int id , int actorid)
        {
            int rows = 0;
            query = "Delete from Movies where MovieID = @id and ActorID = @actorid;";

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@actorid", SqlDbType.Int).Value = actorid;

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

        public bool AddMovieCast(MovieCast movie)
        {
            int rows = 0;
            query = "Insert into MovieCast (MovieID , ActorID, Role) values (@movieid,@actorid,@role);";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            MovieCastVariablesSet(movie);



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


        


    }
}
