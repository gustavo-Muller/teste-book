using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBook.Business.Model;

namespace TesteBook.Business.Interface
{
    public interface IBookApi
    {
        Task<TResult> Get<TResult>(string parametros);
    }
}
