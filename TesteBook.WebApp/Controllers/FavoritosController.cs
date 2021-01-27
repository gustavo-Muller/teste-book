using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TesteBook.Business.Interface;
using TesteBook.Business.Model;
using TesteBook.WebApp.Model;

namespace TesteBook.WebApp.Controllers
{
    public class FavoritosController : Controller
    {
        private readonly IBookService _service;

        public FavoritosController(IBookService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var volumesFavoritados = _service.ObtenhaFavoritos().Result;
            var volumesModelView = new VolumesModelView()
            {
                Volumes = volumesFavoritados.Select(v => Converta(v))
            };

            return View("Index", volumesModelView);
        }

        [HttpPost]
        [Route("deletar/id={id}")]
        public IActionResult Deletar(string id)
        {
            _service.DeleteFavorito(id);
            TempData["excluido"] = true;
            
            return Index();
        }

        private VolumeModelView Converta(Volume volume)
        {
            return new VolumeModelView()
            {
                Id = volume.Id,
                Favoritado = true,
                Title = volume.Title,
                Description = !string.IsNullOrEmpty(volume.Description) && volume.Description.Length > 400 ? volume.Description.Substring(0, 400) : volume.Description,
                Thumbnail = volume.Thumbnail
            };
        }
    }
}
