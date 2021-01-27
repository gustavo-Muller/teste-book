using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TesteBook.Business.Interface;
using TesteBook.Business.Model;

namespace TesteBook.API.Controllers
{
    [ApiController]
    [Route("v1/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<BooksResult>> Get([FromQuery] string parametro)
        {
            var booksResult = _service.ObtenhaLivros(parametro);
            return await booksResult;
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<ActionResult> Favorite(FavoriteParams param)
        {
            if (ModelState.IsValid)
            {
                await _service.FavoriteLivro(param.Id);
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<ActionResult<List<Volume>>> GetFavoritos()
        {
            var booksResult = _service.ObtenhaFavoritos();
            return await booksResult;
        }

        [HttpDelete]
        [Route("favorite")]
        public void DeleteFavorito([FromQuery] string id)
        {
            _service.DeleteFavorito(id);
        }

    }
}
 