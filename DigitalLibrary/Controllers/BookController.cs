using DigitalLibrary.Enums;
using DigitalLibrary.Models;
using DigitalLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;
        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ViewResult> GetAllBooks()
        {
            var data=await _bookRepository.GetAllBooks();
            return View(data);
        }

        public async Task<ViewResult> GetBookById(int id)
        {
            var data=await _bookRepository.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBook(string title, string author)
        {
            return _bookRepository.SearchBook(title, author);
        }

        [Authorize]
        public ViewResult AddNewBook(bool isSuccess=false, int bookId=0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;

            var model = new BookModel()
            {
                LanguagesId = 2
            };
            //ViewBag.Language = new SelectList(await _languageRepository.GetAllLanguage(), "Id", "Name");

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {

            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "book/cover/";
                    bookModel.CoverPhotoUrl = await UploadFile(folder, bookModel.CoverPhoto);
                }

                if (bookModel.GalleryFiles != null)
                {
                    string folder = "book/files/";

                    bookModel.GalleryModel = new List<GalleryModel>();
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            Url = await UploadFile(folder, file)
                        };
                        bookModel.GalleryModel.Add(gallery);
                    };
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "book/pdf/";
                    bookModel.BookPdfUrl = await UploadFile(folder, bookModel.BookPdf);
                }

                var id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            ModelState.AddModelError("","This is custom validation error");

            return View();
        }

        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;


            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
           
            return "/" + folderPath;
        }
    }
}
            //var group1 = new SelectListGroup() { Name = "First Group" };
            //var group2 = new SelectListGroup() { Name = "Second Group" };
            //var group3 = new SelectListGroup() { Name = "Third Group", Disabled=true};

            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Value="1", Text="Pabnai", Selected=true, Group=group1},
            //    new SelectListItem(){Value="2", Text="Sylheti", Group=group1 },
            //    new SelectListItem(){Value="3", Text="Barisal",  Group=group2 },
            //    new SelectListItem(){Value="4", Text="Comilla", Disabled=true,  Group=group2},
            //    new SelectListItem(){Value="5", Text="Chapai",  Group=group3},
            //    new SelectListItem(){Value="6", Text="Chattogram",  Group=group3}
            //};

            //ViewBag.Language = GetLanguages().Select(x => new SelectListItem()
            //{
            //    Text = x.Text,
            //    Value = x.Id.ToString()
            //});

            //ViewBag.Language = new SelectList(GetLanguages(), "Id", "Text",1);

            //ViewBag.Language = new SelectList(GetLanguages(), "Id", "Text");

            //ViewBag.Language = new SelectList(new List<string>() { "English", "Bengali", "Hindi", "Urdu" });

            //ViewBag.Language = new List<string>() {"English","Bengali","Hindi","Urdu" };
        
            //public List<GetLanguage> GetLanguages()
        //{
        //    return new List<GetLanguage>()
        //    {
        //        new GetLanguage(){Id=1, Text="English"},
        //        new GetLanguage(){Id=1, Text="Bengali"},
        //        new GetLanguage(){Id=1, Text="Hindi"}
        //    };
        //}


        //string folder = "book/cover/";
        //folder += Guid.NewGuid().ToString() + "_" + bookModel.CoverPhoto.FileName;

        //bookModel.CoverPhotoUrl = "/" + folder;

        //string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

        //await bookModel.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));