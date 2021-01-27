using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBook.Business.Model;

namespace TesteBook.Business.Repository
{
    public interface IBookRepository
    {
        void Favorite(Volume volume);
        Task<List<Volume>> ObtenhaFavoritos();
        Task DeleteFavorito(string id);
    }
}
