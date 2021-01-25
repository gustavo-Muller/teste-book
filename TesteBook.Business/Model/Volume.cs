using System;

namespace TesteBook.Business.Model
{
    public class Volume
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string PrintType { get; set; }
        public string Thumbnail { get; set; }
    }
}
