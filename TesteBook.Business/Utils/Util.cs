using TesteBook.Business.DTO;
using TesteBook.Business.Model;
using System.Linq;

namespace TesteBook.Business.Utils
{
    public static class Util
    {
        public static BooksResult Converta(this BookDTO dto)
        {
            if (dto == null || dto.Items == null) return new BooksResult();

            return new BooksResult()
            {
                TotalItems = dto.TotalItems,
                Volumes = dto.Items.Select(i => i.Converta())
            };
        }

        public static Volume Converta(this VolumeDTO dto)
        {
            return new Volume()
            {
                Id = dto.Id,
                Title = dto.VolumeInfo.Title,
                Description = dto.VolumeInfo.Description,
                Thumbnail = dto.VolumeInfo.ImageLinks.Thumbnail
            };
        }
    }
}
