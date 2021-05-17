using DigitalLibrary.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author Name")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Book Language")]
        public int LanguagesId { get; set; }

        public string Language { get; set; }

        [Required]
        [Display(Name = "Total Page")]
        public int TotalPage { get; set; }

        [Required]
        [Display(Name = "Upload Cover Photo")]
        public IFormFile CoverPhoto { get; set; }

        public string CoverPhotoUrl { get; set; }

        [Required]
        [Display(Name = "Upload Carousel Images")]
        public IEnumerable<IFormFile> GalleryFiles { get; set; }

        public List<GalleryModel> GalleryModel { get; set; }

        [Required]
        [Display(Name = "Upload PDF File")]
        public IFormFile BookPdf { get; set; }

        public string BookPdfUrl { get; set; }

    }
}

        //[DataType(DataType.DateTime)]
        //[Display(Name = "My Custom Field")]
        //public string MyField { get; set; }

        //[MyCustomValidation("mvc", ErrorMessage ="Please include mvc keyword")]
        //[MyCustomValidation(ErrorMessage ="Hello Programmer")]
        //[MyCustomValidation]