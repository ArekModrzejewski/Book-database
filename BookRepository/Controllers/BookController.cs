using BookRepository.Models;
using BookRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
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
            var book = new BookModel();
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(book);
            }   
        }

        public IActionResult BookList()
        {
            var books = _bookManager.GetBooks();
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
