using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBook.Business.Model;

namespace TesteBook.Business.Interface
{
    public interface IBookService
    {
        Task<BooksResult> ObtenhaLivros(string parametro);
        Task FavoriteLivro(string id);
        Task<List<Volume>> ObtenhaFavoritos();
        Task DeleteFavorito(string id);
    }
}
