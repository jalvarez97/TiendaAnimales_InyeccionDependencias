using AnimalesMVC.Models;
using AnimalesMVC.Servicios.Contratos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TiendaAnimales.Models;

namespace TiendaAnimales.Controllers
{
    public class AnimalesController : Controller
    {
        private readonly IAnimalService _ServicioAnimal;
        private readonly ITipoAnimalService _ServicioTipoAnimal;

        public AnimalesController(IAnimalService servicioAnimal
                                , ITipoAnimalService servicioTipoAnimal)
        {
            _ServicioAnimal = servicioAnimal;
            _ServicioTipoAnimal = servicioTipoAnimal;
        }

        public async Task <IActionResult> Index()
        {
            ViewBag.Mensaje = "¡Valor pasado por viewbag!";

            List<Animal> lstAnimals = new List<Animal>();
            //{
            //    new Animal() {IdAnimal = 1, NombreAnimal = "Gato"},
            //    new Animal() {IdAnimal = 2, NombreAnimal = "Perro"},
            //    new Animal() {IdAnimal = 3, NombreAnimal = "Conejo"},
            //    new Animal() {IdAnimal = 4, NombreAnimal = "Hamster"},
            //};

            lstAnimals = await _ServicioAnimal.ObtenerTodosLosAnimales();
            var tiposAnimales = await _ServicioTipoAnimal.ObtenerTodosLosTiposAnimales();
            Random rnd = new Random();    
            int numeroAleatorio = rnd.Next(0, tiposAnimales.Count);

            ViewBag.EjemploBlabla = "El " + tiposAnimales[numeroAleatorio].TipoDescripcion + " es tu animal de la suerte!";

            return View(lstAnimals);
        }

        public async Task<IActionResult> Crear()
        {
            await CargarComboTipoAnimal();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Animal nuevoAnimal)
        {
            if (ModelState.IsValid)
            {
                await _ServicioAnimal.InsertarAnimal(nuevoAnimal);
                return RedirectToAction("Index"); // Redirigir a la acción "Index"
            }
            await CargarComboTipoAnimal();


            return View(nuevoAnimal);
        }        


        private async Task CargarComboTipoAnimal()
        {
            var tiposAnimales = await _ServicioTipoAnimal.ObtenerTodosLosTiposAnimales();
            // Agregar la opción predeterminada "Seleccionar..."
            tiposAnimales.Insert(0, new TipoAnimal { IdTipoAnimal = 0, TipoDescripcion = "Seleccionar..." });

            ViewBag.TiposAnimales = new SelectList(tiposAnimales, "IdTipoAnimal", "TipoDescripcion");
        }
    }
}
