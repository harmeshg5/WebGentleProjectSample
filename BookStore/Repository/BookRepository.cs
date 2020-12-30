using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext context;

        public BookRepository(BookStoreContext _context)
        {
            context = _context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            Books book = new Books()
            {
                Author = model.Author,
                Category = model.Category,
                Description = model.Description,
                LanguageId = model.LanguageId,
                Title = model.Title,
                TotalPages = model.TotalPages,
                CoverImageUrl = model.CoverImageUrl,
                BookPDFURL = model.BookPDFUrl
            };
            book.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                book.bookGallery.Add(new BookGallery() { Name = file.Name, URL = file.URL });
            }

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return book.Id;

        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var data = await context.Books.ToListAsync();
            if (data?.Any() == true)
            {
                foreach (var item in data)
                {
                    books.Add(new BookModel()
                    {
                        Author = item.Author,
                        Category = item.Category,
                        Description = item.Description,
                        LanguageId = item.LanguageId,
                        Title = item.Title,
                        Id = item.Id,
                        TotalPages = item.TotalPages,
                        CoverImageUrl = item.CoverImageUrl
                    });
                }
            }
            return books;
        }
        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await context.Books.Select(item => new BookModel()
            {
                Author = item.Author,
                Category = item.Category,
                Description = item.Description,
                LanguageId = item.LanguageId,
                Title = item.Title,
                Id = item.Id,
                TotalPages = item.TotalPages,
                CoverImageUrl = item.CoverImageUrl
            }).Take(count).ToListAsync();
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var data = await context.Books.Where(x => x.Id == id).Select(data => new BookModel()
            {
                Author = data.Author,
                Category = data.Category,
                Language = data.Language.Text,
                Description = data.Description,
                Id = data.Id,
                LanguageId = data.LanguageId,
                Title = data.Title,
                TotalPages = data.TotalPages,
                CoverImageUrl = data.CoverImageUrl,
                BookPDFUrl = data.BookPDFURL,
                Gallery = data.bookGallery.Select(x => new GalleryModel() { Id = x.Id, Name = x.Name, URL = x.URL }).ToList()
            }).FirstOrDefaultAsync();

            return data;
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null; // DataSource().Where(a => a.Title.Contains(title) && a.Author.Contains(authorName)).ToList();
        }
    }
}
