using AnimalesMVC.Models;

namespace AnimalesMVC.Servicios.Contratos
{
    public interface ITipoAnimalService
    {
        Task<List<TipoAnimal>> ObtenerTodosLosTiposAnimales();
        
        Task<TipoAnimal> ObtenerTipoAnimalPorId(int idTipoAnimal);

        Task InsertarTipoAnimal(TipoAnimal tipoAnimal);

        Task ActualizarTipoAnimal(TipoAnimal tipoAnimal);

        Task EliminarTipoAnimal(int idTipoAnimal);
    }
}
