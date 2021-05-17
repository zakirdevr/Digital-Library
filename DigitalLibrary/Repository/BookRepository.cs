using DigitalLibrary.Data;
using DigitalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context = null;
        private readonly IConfiguration _configuration = null;
        public BookRepository(BookContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<int> AddNewBook(BookModel bookModel)
        {
            var newBook = new Books()
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Author = bookModel.Author,
                Description = bookModel.Description,
                Category = bookModel.Category,
                LanguagesId = bookModel.LanguagesId,
                TotalPage = bookModel.TotalPage,
                CoverPhotoUrl = bookModel.CoverPhotoUrl,
                BookPdfUrl = bookModel.BookPdfUrl
            };

            newBook.BookGallery = new List<BookGallery>();
            foreach (var file in bookModel.GalleryModel)
            {
                newBook.BookGallery.Add(new BookGallery()
                {
                    Id = file.Id,
                    Name = file.Name,
                    Url = file.Url
                });
            }

            await _context.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;

        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Description = x.Description,
                Category = x.Category,
                LanguagesId = x.LanguagesId,
                Language = x.Languages.Name,
                TotalPage = x.TotalPage,
                CoverPhotoUrl = x.CoverPhotoUrl
            }).ToListAsync();

        }

        public async Task<List<BookModel>> GetTopBooks(int count)
        {
            return await _context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Description = x.Description,
                Category = x.Category,
                LanguagesId = x.LanguagesId,
                Language = x.Languages.Name,
                TotalPage = x.TotalPage,
                CoverPhotoUrl = x.CoverPhotoUrl
            }).Take(count).ToListAsync();

        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Description = x.Description,
                Category = x.Category,
                LanguagesId = x.LanguagesId,
                Language = x.Languages.Name,
                TotalPage = x.TotalPage,
                CoverPhotoUrl = x.CoverPhotoUrl,
                BookPdfUrl = x.BookPdfUrl,
                GalleryModel = x.BookGallery.Select(g => new GalleryModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    Url = g.Url
                }).ToList()

            }).FirstOrDefaultAsync();
        }
        public List<BookModel> SearchBook(string title, string author)
        {
            return null;
        }
        public string GetAppName()
        {
            return _configuration["AppName"];
        }


        #region
        //public List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel(){Id=1,Title="C Sharp", Author="Zakir",
        //            Description="This is C# Book", Category="Programming",
        //            Language="Bangla", TotalPage=150 },

        //        new BookModel(){Id=2,Title="Asp.Net Core", Author="Zahid",
        //            Description="This is Asp.Net Core Book",
        //            Category="Framework", Language="English",
        //            TotalPage=250 },

        //        new BookModel(){Id=3,Title="React", Author="Jewel",
        //            Description="This is React Book", Category="Library",
        //            Language="Chinese", TotalPage=350 },

        //        new BookModel(){Id=4,Title="Angular", Author="Sohel",
        //            Description="This is Angular Book",
        //            Category="Framework", Language="Hindi", TotalPage=450 },

        //        new BookModel(){Id=5,Title="CPP", Author="Sazid",
        //            Description="This is C++ Book", Category="Programming",
        //            Language="Urdu", TotalPage=550 },

        //        new BookModel(){Id=6,Title="JavaScript", Author="Sabbir",
        //            Description="This is JavaScript Book",
        //            Category="Programming", Language="Arabic",
        //            TotalPage=650 }
        //    };
        //}
        #endregion
    }
}
