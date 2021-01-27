using System;
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
                Publisher = dto.VolumeInfo.Publisher,
                Description = dto.VolumeInfo.Description,
                PageCount = dto.VolumeInfo.PageCount,
                PrintType = dto.VolumeInfo.PrintType,
                Thumbnail = dto.VolumeInfo.ImageLinks.Thumbnail
            };
        }
    }
}
