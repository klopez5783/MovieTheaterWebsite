using Antlr.Runtime.Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace MovieTheater
{
    public class ActorsDataAccess : BaseTier
    {
        public ActorsDataAccess() : base()
        {
            
        }

        public Actors ActorVariablesSet(Actors actor)
        {
            cmd.Parameters.Add("@firstname", SqlDbType.NVarChar,50).Value = actor.FirstName;
            cmd.Parameters.Add("@lastname", SqlDbType.NVarChar, 50).Value = actor.LastName;
            cmd.Parameters.Add("@gender", SqlDbType.NVarChar, 50).Value = actor.Gender;
            if(actor.ActorIMG != null)
            {
                cmd.Parameters.Add("@actorimg", SqlDbType.Image).Value = actor.ActorIMG;
            }

            return actor;
        } 

        public List<Actors> ListActors()
        {
            Actors actor = null;
            List<Actors> actorslist = null;
            query = "Select * from Actors;";
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
                    actorslist = new List<Actors>();
                    while (reader.Read())
                    {
                        actor = new Actors();
                        actor.ActorID = (int)reader["ActorID"];
                        actor.FirstName = reader["FirstName"].ToString();
                        actor.LastName = reader["LastName"].ToString();
                        actor.Gender = reader["Gender"].ToString();

                        actorslist.Add(actor);
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

            return actorslist;
        } // ends ListActors



        public bool AddActor(Actors actor)
        {
            int rows = 0;
            if (actor.ActorIMG == null)
            {
                query = "Insert into Actors (FirstName, LastName, Gender) Values (@firstname,@lastname,@gender)";
            }
            else
            {
                query = "Insert into Actors (FirstName, LastName, Gender, ActorIMG) Values (@firstname,@lastname,@gender,@actorimg)";
            }

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);

            ActorVariablesSet(actor);

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

        } // ends AddActor


        public Actors FindActor(int id)
        {
            Actors actor = new Actors();
            query = "Select * from Actors where ActorID = @actorid";
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@actorid", SqlDbType.Int).Value = id;
            try
            {
                // opens connection to database
                conn.Open();
                // executes the query and gets data, then puts in into the reader
                reader = cmd.ExecuteReader();
                // checks if data was returned
                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        actor = new Actors();
                        actor.ActorID = (int)reader["ActorID"];
                        actor.FirstName = reader["FirstName"].ToString();
                        actor.LastName = reader["LastName"].ToString();
                        actor.Gender = reader["Gender"].ToString();
                        if (reader["ActorIMG"] != DBNull.Value)
                        {
                            actor.ActorIMG = (byte[])reader["ActorIMG"];
                        }
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

            return actor;
        } // ends FindActor



        public bool EditMovie(Actors actors)
        {
            int rows = 0;
            if (actors.ActorIMG != null)
            {
                query = "Update Actors " +
                    "Set FirstName = @firstname,LastName = @lastname,Gender=@gender,ActorIMG = @actorimg where ActorID = @actorid";
            }
            else
            {
                query = "Update Actors " +
                    "Set FirstName = @firstname,LastName = @lastname,Gender=@gender where ActorID = @actorid";
            }
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@actorid", SqlDbType.Int).Value = actors.ActorID;

            ActorVariablesSet(actors);
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

        public byte[] GetActorImage(int id)
        {
            query = "Select ActorIMG from Actors where ActorID = @id;";
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
                    img = (byte[])reader["ActorIMG"];

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
        }// ends get img


        public bool DeleteActor(int id)
        {
            int rows = 0;
            query = "Delete from Actors where ActorID = @id";
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
        }

    }

}
