using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Data
{
    public class BookGallery
    {
        public int Id { get; set; }
        public int BooksId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Books Books { get; set; }
    }
}
