using AnimalesMVC.Servicios.Configuracion;
using AnimalesMVC.Servicios.Contratos;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using TiendaAnimales.Models;

namespace AnimalesMVC.Servicios.Implementacion
{
    public class AnimalService : IAnimalService
    {
        private readonly ConfiguracionConexion _conexion;

        public AnimalService(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion = conexion.Value;
        }

        public async Task<List<Animal>> ObtenerTodosLosAnimales()
        {
            try
            {
                List<Animal> animales = new List<Animal>();

                using (SqlConnection connection = new SqlConnection(_conexion.CadenaBBDD))
                {
                    string query = "SELECT IdAnimal, NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento FROM Animal";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Animal animal = new Animal
                                {
                                    IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                                    NombreAnimal = reader["NombreAnimal"].ToString(),
                                    Raza = reader["Raza"].ToString(),
                                    RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                                    FechaNacimiento = (reader["FechaNacimiento"] != DBNull.Value) ? Convert.ToDateTime(reader["FechaNacimiento"]) : (DateTime?)null
                                };
                                animales.Add(animal);
                            }
                        }
                    }
                }

                return animales;
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public async Task<Animal> ObtenerAnimalPorId(int idAnimal)
        {
            try
            {
                Animal animal = null;

                using (SqlConnection connection = new SqlConnection(_conexion.CadenaBBDD))
                {
                    string query = "SELECT IdAnimal, NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento FROM Animal WHERE IdAnimal = @IdAnimal";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdAnimal", idAnimal);

                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                animal = new Animal
                                {
                                    IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                                    NombreAnimal = reader["NombreAnimal"].ToString(),
                                    Raza = reader["Raza"].ToString(),
                                    RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                                    FechaNacimiento = (reader["FechaNacimiento"] != DBNull.Value) ? Convert.ToDateTime(reader["FechaNacimiento"]) : (DateTime?)null
                                };
                            }
                        }
                    }
                }

                return animal;
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public async Task InsertarAnimal(Animal animal)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_conexion.CadenaBBDD))
                {
                    string query = "INSERT INTO Animal (NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento) " +
                                   "VALUES (@NombreAnimal, @Raza, @RIdTipoAnimal, @FechaNacimiento)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                        command.Parameters.AddWithValue("@Raza", animal.Raza);
                        command.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                        command.Parameters.AddWithValue("@FechaNacimiento", (object)animal.FechaNacimiento ?? DBNull.Value);

                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task ActualizarAnimal(Animal animal)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_conexion.CadenaBBDD))
                {
                    string query = "UPDATE Animal " +
                                   "SET NombreAnimal = @NombreAnimal, Raza = @Raza, RIdTipoAnimal = @RIdTipoAnimal, FechaNacimiento = @FechaNacimiento " +
                                   "WHERE IdAnimal = @IdAnimal";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
                        command.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                        command.Parameters.AddWithValue("@Raza", animal.Raza);
                        command.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                        command.Parameters.AddWithValue("@FechaNacimiento", (object)animal.FechaNacimiento ?? DBNull.Value);

                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task EliminarAnimal(int idAnimal)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_conexion.CadenaBBDD))
                {
                    string query = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdAnimal", idAnimal);

                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }           
        }
    }
}
