﻿namespace TesteBook.Business.DTO
{
    public class VolumeInfoDTO
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string PrintType { get; set; }
        public ImageLinksDTO ImageLinks { get; set; }
    }
}
