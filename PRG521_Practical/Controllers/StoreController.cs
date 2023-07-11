using PRG521_Practical.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace PRG521_Practical.Controllers
{
    [AllowAnonymous]
    public class StoreController : Controller
    {
        private readonly PRG521_PracticalContext _context;

        public StoreController(PRG521_PracticalContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(string searchString, string minPrice, string maxPrice)
        {
            var books = _context.Books.Select(b => b);

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(minPrice))
            {
                var min = int.Parse(minPrice);
                books = books.Where(b => b.Price >= min);
            }

            if (!string.IsNullOrEmpty(maxPrice))
            {
                var max = int.Parse(maxPrice);
                books = books.Where(b => b.Price <= max);
            }

            return View(await books.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public IActionResult Search()
        {
            return View();
        }
    }
}
