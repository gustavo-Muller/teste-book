using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [Route("")]
        public async Task<ActionResult<BooksResult>> Get(string parametros)
        {
            var booksResult = _service.ObtenhaLivros(parametros);
            return await booksResult;
        }

        [HttpPost]
        [Route("favoritos/id={id}")]
        public async Task<ActionResult> Favorite(string id)
        {
            if (ModelState.IsValid)
            {
                await _service.FavoriteLivro(id);
                return null;
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("favoritos")]
        public async Task<ActionResult<List<Volume>>> GetFavoritos()
        {
            var booksResult = _service.ObtenhaFavoritos();
            return await booksResult;
        }

        [HttpDelete]
        [Route("favoritos/id={id}")]
        public void DeleteFavorito(string id)
        {
            _service.DeleteFavorito(id);
        }

    }
}
