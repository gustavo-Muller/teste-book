using MaximaTechCriptografia.Business;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<BooksResult>> Get([FromQuery] string parametro)
        {
            try
            {
                return await _service.ObtenhaLivros(parametro);
            }
            catch (Exception ex)
            {
                UtilitarioLogger.GraveLog(ex.Message);
                return BadRequest(new BooksResult());
            }
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<ActionResult> Favorite(FavoriteParams param)
        {
            try
            {
                await _service.FavoriteLivro(param.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                UtilitarioLogger.GraveLog(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<ActionResult<List<Volume>>> GetFavoritos()
        {
            try
            {
                return await _service.ObtenhaFavoritos();
            }
            catch (Exception ex)
            {
                UtilitarioLogger.GraveLog(ex.Message);
                return BadRequest(new List<Volume>());
            }
        }

        [HttpDelete]
        [Route("favorite")]
        public async Task<ActionResult> DeleteFavorito([FromQuery] string id)
        {
            try
            {
                await _service.DeleteFavorito(id);
                return Ok();
            }
            catch(Exception ex)
            {
                UtilitarioLogger.GraveLog(ex.Message);
                return BadRequest();
            }
        }

    }
}
