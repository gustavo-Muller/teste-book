using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TesteBook.Business.Interface;
using TesteBook.Business.Model;
using TesteBook.WebApp.Model;
using TesteBook.WebApp.Models;

namespace TesteBook.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _service;

        public HomeController(IBookService service)
        {
            _service = service;
        }

        public IActionResult Index(VolumesModelView model)
        {
            if (!model.Volumes.Any())
            {
                model.Volumes = ObtenhaVolumesViewModel(string.Empty);
            }

            return View("Index", model);
        }

        [HttpGet("pesquisar")]
        public IActionResult Pesquisar(string parametros)
        {
            return Index(new VolumesModelView() { Volumes = ObtenhaVolumesViewModel(parametros) });
        }

        [HttpPost("favorite/id={id}")]
        public IActionResult favoritar(string id)
        {
            _service.FavoriteLivro(id);
            return Index(new VolumesModelView());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<VolumeModelView> ObtenhaVolumesViewModel(string parametros)
       {
            var booksResult = _service.ObtenhaLivros(parametros).Result;
            var favoritos = _service.ObtenhaFavoritos().Result;
            return booksResult.Volumes.Select(v => Converta(v, favoritos.Exists(f => f.Id.Equals(v.Id))));
        }

        private VolumeModelView Converta(Volume volume, bool favoritado)
        {
            return new VolumeModelView()
            {
                Id = volume.Id,
                Favoritado = favoritado,
                Title = volume.Title,
                Publisher = volume.Publisher,
                Description = !string.IsNullOrEmpty(volume.Description) && volume.Description.Length > 400 ? volume.Description.Substring(0, 400) : volume.Description,
                PageCount = volume.PageCount,
                PrintType = volume.PrintType,
                Thumbnail = volume.Thumbnail
            };
        }
    }
}
