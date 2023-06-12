using BookDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookDatabase.Pages.Forms
{
    public class AddBookModel : PageModel
    {
        [BindProperty]
        public BookModel Book { get; set; }
        public void OnGet()
        {
        }
    }
}
