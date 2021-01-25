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

        public IActionResult Index(HomeModelView model)
        {
            if(!model.Volumes.Any())
            {
                var booksResult = _service.ObtenhaLivros(string.Empty).Result;
                model.Volumes = booksResult.Volumes.Select(v => Converta(v));
            }

            return View("Index", model);
        }

        [HttpGet("pesquisar")]
        public IActionResult Pesquisar(string parametros)
        {
            var booksResult = _service.ObtenhaLivros(parametros).Result;
            var volumesModelView = booksResult.Volumes.Select(v => Converta(v));

            return Index(new HomeModelView() { Volumes = volumesModelView });
        }

        [HttpPost("favoritos/id={id}")]
        public IActionResult favoritar(string id)
        {
            _service.FavoriteLivro(id);
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private VolumeModelView Converta(Volume volume)
        {
            return new VolumeModelView()
            {
                Id = volume.Id,
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
