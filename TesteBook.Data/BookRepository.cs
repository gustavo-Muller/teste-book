using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBook.Business.Model;
using TesteBook.Business.Repository;

namespace TesteBook.Data
{
    public class BookRepository : IBookRepository
    {
        public void Favorite(Volume volume)
        {
            using (var dbContext = new BookContext())
            {
                dbContext.Volumes.Add(volume);
                dbContext.SaveChanges();
            }
        }

        public async Task<List<Volume>> ObtenhaFavoritos()
        {
            using (var dbContext = new BookContext())
            {
                return await dbContext.Volumes.ToListAsync();
            }
        }

        public async Task DeleteFavorito(string id)
        {
            using (var dbContext = new BookContext())
            {
                var volume = await dbContext.Volumes.SingleAsync(v => v.Id.Equals(id));
                dbContext.Remove(volume);
                dbContext.SaveChanges();
            }
        }
    }
}
