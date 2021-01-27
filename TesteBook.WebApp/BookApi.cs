using MaximaTechCriptografia.Business;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TesteBook.Business.Interface;

namespace TesteBook.WebApp
{
    public class BookApi : IBookApi
    {
        private string _uri = "http://localhost:58915/v1/books/";

        public async Task<TResult> Get<TResult>(string uri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(MonteUri(uri));
                    response.EnsureSuccessStatusCode();

                    var responseString = await response.Content.ReadAsStringAsync();

                    TResult result = JsonConvert.DeserializeObject<TResult>(responseString);
                    return result;
                }
                catch (Exception ex)
                {
                    UtilitarioLogger.GraveLog(ex.Message);
                }

                return default;
            }
        }

        public async Task Post<TParam>(TParam value, string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var stringContent = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(MonteUri(uri), stringContent);

                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                UtilitarioLogger.GraveLog(ex.Message);
            }
        }

        public async Task Delete(string uri)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.DeleteAsync(MonteUri(uri));
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                UtilitarioLogger.GraveLog(ex.Message);
            }
        }

        private string MonteUri(string uri)
        {
            return $"{_uri}{uri}";
        }
    }
}
