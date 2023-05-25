using System;
using System.Collections.Generic;
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

        }

    }
}