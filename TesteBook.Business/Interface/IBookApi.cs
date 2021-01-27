using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TesteBook.Business.Model;

namespace TesteBook.Business.Interface
{
    public interface IBookApi
    {
        Task<TResult> Get<TResult>(string uri);
        Task Post<TParam>(TParam value, string uri);
        Task Delete(string uri);
    }
}
