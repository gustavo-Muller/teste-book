using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            if (!_bookRepository.ExisteLivro(id).Result)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_uri);
                    var result = await client.GetAsync($"volumes/{id}");

                    if (result.IsSuccessStatusCode)
                    {
                        var resultString = await result.Content.ReadAsStringAsync();
                        var volumeDTO = JsonConvert.DeserializeObject<VolumeDTO>(resultString);
                        _bookRepository.Favorite(volumeDTO.Converta());
                    }
                }
            }
        }

        public async Task<List<Volume>> ObtenhaFavoritos()
        {
            return await _bookRepository.ObtenhaFavoritos();
        }

        public async Task DeleteFavorito(string id)
        {
            await _bookRepository.DeleteFavorito(id);
        }

    }
}
