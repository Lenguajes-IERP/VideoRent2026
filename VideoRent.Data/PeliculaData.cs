using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using VideoRent.Domain;

namespace VideoRent.Data
{
    public class PeliculaData
    {
        private String connectionString;

        public PeliculaData(String connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Insertar(Pelicula pelicula)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmdPelicula = connection.CreateCommand();
                cmdPelicula.CommandText = "InsertPelicula";
                cmdPelicula.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramPeliculaId = 
                    new SqlParameter("@pelicula_id", System.Data.SqlDbType.Int);
                paramPeliculaId.Direction = System.Data.ParameterDirection.Output;
                cmdPelicula.Parameters.Add(paramPeliculaId);

                cmdPelicula.Parameters.AddWithValue("@titulo", pelicula.Titulo);
                cmdPelicula.Parameters.AddWithValue("@subtitulada", pelicula.Subtitulada);
                cmdPelicula.Parameters.AddWithValue("@estreno", pelicula.Estreno);
                cmdPelicula.Parameters.AddWithValue("@generoId", pelicula.Genero.GeneroId);

                SqlTransaction sqlTransaction = connection.BeginTransaction();
                cmdPelicula.Transaction = sqlTransaction;
                try
                {
                    cmdPelicula.ExecuteNonQuery();

                    pelicula.PeliculaId =
                        Int32.Parse(cmdPelicula.Parameters["@pelicula_id"].Value.ToString());
                    foreach (var actor in pelicula.Actores)
                    {
                        SqlCommand cmdPeliculaActor = connection.CreateCommand();
                        cmdPeliculaActor.Transaction = sqlTransaction;
                        cmdPeliculaActor.CommandText = "InsertPeliculaActor";
                        cmdPeliculaActor.CommandType = System.Data.CommandType.StoredProcedure;
                        cmdPeliculaActor.Parameters.AddWithValue("@pelicula_id", pelicula.PeliculaId);
                        cmdPeliculaActor.Parameters.AddWithValue("@actor_id", actor.ActorId);
                        cmdPeliculaActor.ExecuteNonQuery();
                    }
                    sqlTransaction.Commit();

                }
                catch (SqlException exc)
                {
                    if (sqlTransaction != null)
                    {
                        sqlTransaction.Rollback();
                    }
                    throw;
                }

                
            }

        }//Insertar
    }
}
