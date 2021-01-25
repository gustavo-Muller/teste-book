using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TesteBook.Business.Interface;
using TesteBook.Business.Model;

namespace TesteBook.WebApp
{
    public class BookApi : IBookApi
    {
        private string _uri = "http://localhost:58915/v1/books";

        public async Task<TResult> Get<TResult>(string parametros)
        {
            TResult result;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_uri}?parametros={parametros}");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<TResult>(responseString);

                return result;
            }
        }
    }
}
