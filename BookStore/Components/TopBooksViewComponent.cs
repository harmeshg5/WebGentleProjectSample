using BookStore.Repository;
using BookStore.Repository.BookRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository bookRepository;

        public TopBooksViewComponent(IBookRepository _bookRepository)
        {
            bookRepository = _bookRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await bookRepository.GetTopBooksAsync(count);
            return View(books);
        }
    }
}
