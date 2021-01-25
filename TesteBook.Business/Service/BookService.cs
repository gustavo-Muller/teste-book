using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TesteBook.Business.DTO;
using TesteBook.Business.Interface;
using TesteBook.Business.Model;
using TesteBook.Business.Repository;
using TesteBook.Business.Utils;

namespace TesteBook.Service
{
    public class BookService : IBookService
    {
        private const string _uri = "https://www.googleapis.com/books/v1/";
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BooksResult> ObtenhaLivros(string parametro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_uri);
                var result = await client.GetAsync($"volumes?q={parametro}");

                if (result.IsSuccessStatusCode)
                {
                    var resultString = await result.Content.ReadAsStringAsync();
                    var booksDTO = JsonConvert.DeserializeObject<BookDTO>(resultString);

                    return booksDTO.Converta();
                }

            }

            return new BooksResult();
        }


        public async Task FavoriteLivro(string id)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_uri);
                var result = await client.GetAsync($"volumes?q={{id={id}}}");

                if (result.IsSuccessStatusCode)
                {
                    var resultString = await result.Content.ReadAsStringAsync();
                    var booksDTO = JsonConvert.DeserializeObject<BookDTO>(resultString);
                    var books = booksDTO.Converta();

                    _bookRepository.Favorite(books.Volumes.FirstOrDefault());
                }
            }
        }


        public async Task<List<Volume>> ObtenhaFavoritos()
        {
            var favoritos = await _bookRepository.ObtenhaFavoritos();
            return favoritos;
        }

        public void DeleteFavorito(string id)
        {
            _bookRepository.DeleteFavorito(id);
        }

    }
}
