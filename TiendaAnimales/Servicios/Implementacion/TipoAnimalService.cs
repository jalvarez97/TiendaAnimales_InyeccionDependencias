using AnimalesMVC.Models;
using AnimalesMVC.Servicios.Configuracion;
using AnimalesMVC.Servicios.Contratos;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace AnimalesMVC.Servicios.Implementacion
{
    public class TipoAnimalService : ITipoAnimalService
    {
        private readonly ConfiguracionConexion _conexion;

        public TipoAnimalService(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion = conexion.Value;
        }

        public async Task<List<TipoAnimal>> ObtenerTodosLosTiposAnimales()
        {
            List<TipoAnimal> tiposAnimales = new List<TipoAnimal>();

            using (SqlConnection connection = new SqlConnection(_conexion.CadenaBBDD))
            {
                string query = "SELECT IdTipoAnimal, TipoDescripcion FROM TipoAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            TipoAnimal tipoAnimal = new TipoAnimal
                            {
                                IdTipoAnimal = Convert.ToInt32(reader["IdTipoAnimal"]),
                                TipoDescripcion = reader["TipoDescripcion"].ToString()
                            };
                            tiposAnimales.Add(tipoAnimal);
                        }
                    }
                }
            }

            return tiposAnimales;
        }
        
        public async Task <TipoAnimal> ObtenerTipoAnimalPorId(int idTipoAnimal)
        {
            TipoAnimal tipoAnimal = null;

            using (SqlConnection connection = new SqlConnection(_conexion.CadenaBBDD))
            {
                string query = "SELECT IdTipoAnimal, TipoDescripcion FROM TipoAnimal WHERE IdTipoAnimal = @IdTipoAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoAnimal", idTipoAnimal);

                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            tipoAnimal = new TipoAnimal
                            {
                                IdTipoAnimal = Convert.ToInt32(reader["IdTipoAnimal"]),
                                TipoDescripcion = reader["TipoDescripcion"].ToString()
                            };
                        }
                    }
                }
            }

            return tipoAnimal;
        }

        public async Task InsertarTipoAnimal(TipoAnimal tipoAnimal)
        {
            using (SqlConnection connection = new SqlConnection(_conexion.CadenaBBDD))
            {
                string query = "INSERT INTO TipoAnimal (TipoDescripcion) VALUES (@TipoDescripcion)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TipoDescripcion", tipoAnimal.TipoDescripcion);

                    connection.Open();
                    await  command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task ActualizarTipoAnimal(TipoAnimal tipoAnimal)
        {
            using (SqlConnection connection = new SqlConnection(_conexion.CadenaBBDD))
            {
                string query = "UPDATE TipoAnimal SET TipoDescripcion = @TipoDescripcion WHERE IdTipoAnimal = @IdTipoAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoAnimal", tipoAnimal.IdTipoAnimal);
                    command.Parameters.AddWithValue("@TipoDescripcion", tipoAnimal.TipoDescripcion);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EliminarTipoAnimal(int idTipoAnimal)
        {
            using (SqlConnection connection = new SqlConnection(_conexion.CadenaBBDD))
            {
                string query = "DELETE FROM TipoAnimal WHERE IdTipoAnimal = @IdTipoAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoAnimal", idTipoAnimal);

                    connection.Open();
                    await  command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
