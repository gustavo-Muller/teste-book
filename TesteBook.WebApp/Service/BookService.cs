using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBook.Business.Interface;
using TesteBook.Business.Model;

namespace TesteBook.WebApp.Service
{
    public class BookService : IBookService
    {
        private readonly IBookApi _api;
        public BookService(IBookApi api)
        {
            _api = api;
        }

        public void DeleteFavorito(string id)
        {
            //_api.DeleteFavorito(id);
        }

        public async Task FavoriteLivro(string id)
        {
            //await _api.FavoriteLivro(id);
        }

        public async Task<List<Volume>> ObtenhaFavoritos()
        {
            return await _api.Get<List<Volume>>("");
        }

        public async Task<BooksResult> ObtenhaLivros(string parametro)
        {
            if (string.IsNullOrEmpty(parametro)) parametro = "{}";
            return await _api.Get<BooksResult>(parametro);
        }
    }
}
