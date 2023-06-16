using BookRepository.Data;
using BookRepository.Models;
using System.Linq;

namespace BookRepository.Repositories
{
    public class BookManager
    {
        private ApplicationDbContext _context;
        public BookManager(ApplicationDbContext context) 
        { 
            _context = context;
        }
    public BookManager AddBook(BookModel bookModel)
        {
            _context.Books.Add(bookModel);
            _context.SaveChanges();
            return this;
        }

        public BookManager RemoveBook(int id)
        {
            var bookToDelete = _context.Books.SingleOrDefault(book => book.Id == id);
            _context.Books.Remove(bookToDelete);
            _context.SaveChanges();
            return this;
        }

        public BookManager UpdateBook(BookModel bookModel)
        {
            _context.Update(bookModel);
            _context.SaveChanges();
            return this;
        }

        public BookManager ChangeTitle(int id, string newTitle)
        {
            var bookToChange = GetBook(id);
            try
            {
                bookToChange.Title = newTitle;
                UpdateBook(bookToChange);
            }
            catch (Exception ex)
            {
                if (newTitle == null)
                {
                    bookToChange.Title = "No Title";
                }
                bookToChange.Title = newTitle;
                UpdateBook(bookToChange);
            }
            return this;
        }

        public BookModel GetBook(int id)
        {
            var book = _context.Books.SingleOrDefault(book=> book.Id == id);
            return book;
        }

        public List<BookModel> GetBooks()
        {
            var books = _context.Books.ToList();
            return books;
        }
    }
}
