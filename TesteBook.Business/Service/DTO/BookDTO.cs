using System.Collections.Generic;

namespace TesteBook.Business.DTO
{
    public class BookDTO
    {
        public int TotalItems { get; set; }
        public List<VolumeDTO> Items { get; set; }
    }
}
