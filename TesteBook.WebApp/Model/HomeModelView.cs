using System.Collections.Generic;

namespace TesteBook.WebApp.Model
{
    public class HomeModelView
    {
        public IEnumerable<VolumeModelView> Volumes { get; set; } = new List<VolumeModelView>();
    }
}
