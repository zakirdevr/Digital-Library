using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Data
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int LanguagesId { get; set; }
        public int TotalPage { get; set; }
        public Languages Languages { get; set; }
        public string CoverPhotoUrl { get; set; }
        public ICollection<BookGallery> BookGallery { get; set; }
        public string BookPdfUrl { get; set; }
    }
}
