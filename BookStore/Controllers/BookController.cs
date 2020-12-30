using BookStore.Models;
using BookStore.Repository;
using BookStore.Repository.BookRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly IWebHostEnvironment environment;

        public ILanguageRepository languageRepository { get; }

        public BookController(IBookRepository bookRepository, ILanguageRepository _languageRepository, IWebHostEnvironment _environment)
        {
            _bookRepository = bookRepository;
            languageRepository = _languageRepository;
            environment = _environment;
        }
        public string Index()
        {
            return "Hello Harmesh";
        }

        public async Task<IActionResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        public List<BookModel> SearchBook(string title, string authorname)
        {
            return _bookRepository.SearchBook(title, authorname);
        }

        public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModel();
            ViewBag.Language = new SelectList(await languageRepository.GetLanguages(), "Id", "Text");
            ViewBag.isSuccess = isSuccess;
            var dataTest = await languageRepository.GetLanguages();
            ViewBag.bookId = bookId;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    model.CoverImageUrl = await UploadImage(folder, model.CoverPhoto);
                }

                if (model.GalleryFiles != null && model.GalleryFiles.Count > 0)
                {
                    string folder = "books/gallery/";
                    model.Gallery = new List<GalleryModel>();
                    foreach (var file in model.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        model.Gallery.Add(gallery);
                    }
                }

                if (model.BookPDF != null)
                {
                    string folder = "books/pdf/";
                    model.BookPDFUrl = await UploadImage(folder, model.BookPDF);
                }

                int id = await _bookRepository.AddNewBook(model);
                if (id > 0)
                {
                    return RedirectToAction("AddNewBook", new { isSuccess = true, bookId = id });
                }
            }
            ViewBag.Language = new SelectList(await languageRepository.GetLanguages(), "Id", "Text");
            ViewBag.isSuccess = false;
            ViewBag.bookId = 0;
            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(environment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

    }
}
