using BookRepository.Models;
using BookRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace BookRepository.Controllers
{
    public class BookController : Controller
    {
        private BookManager _bookManager;
        public BookController(BookManager bookManager)
        {
            _bookManager = bookManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookModel book)
        {
            try
            {
                _bookManager.AddBook(book);
                return RedirectToAction("BookList");
            }
            catch (Exception ex)
            {
                return View(book);
            }   
        }

        public IActionResult BookList(string sortOrder,
            string currentFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["AuthorSortParm"] = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            ViewData["TitleSortParm"] = sortOrder == "title" ? "title_desc" : "title";

            ViewData["CurrentFilter"] = currentFilter;

            var books = from b in _bookManager.GetBooks()
                           select b;
            if (!String.IsNullOrEmpty(currentFilter))
            {
                books = books.Where(b => b.Genre.Contains(currentFilter));
            }
            switch (sortOrder)
            {
                case "author_desc":
                    books = books.OrderByDescending(b => b.Author);
                    break;
                case "title":
                    books = books.OrderBy(b => b.Title);
                    break;
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                default:
                    books = books.OrderBy(b => b.Author);
                    break;
            }
            return View(books);

        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var book = _bookManager.GetBook(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult RemoveConfirm(int id)
        {
            try
            {
                _bookManager.RemoveBook(id);
                return RedirectToAction("BookList");
            }
            catch (Exception ex)
            {
                return View("Remove", id);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _bookManager.GetBook(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(BookModel book)
        {
            _bookManager.UpdateBook(book);
            return RedirectToAction("BookList");

        }
    }
}
