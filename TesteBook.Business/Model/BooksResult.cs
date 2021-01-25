using System.Collections.Generic;

namespace TesteBook.Business.Model
{
    public class BooksResult
    {
        public int TotalItems { get; set; }
        public IEnumerable<Volume> Volumes { get; set; }
    }
}
