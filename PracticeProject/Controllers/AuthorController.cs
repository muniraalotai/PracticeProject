using Microsoft.AspNetCore.Mvc;
using PracticeProject.Models;
using PracticeProject.Repositories;

namespace PracticeProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookStoreRepository<Author> authorRepository;
        
        public AuthorController(IBookStoreRepository<Author> bookStoreRepository)
        {
            this.authorRepository = bookStoreRepository;
        }

        // GET: Author
        public ActionResult Index()
        {
            var authors = authorRepository.List();
            return View(authors);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FullName")] Author author)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Author/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FullName")] Author author)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(author); 
            }
        }

        // GET: Author/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(); 
            }
        }
        
    }
}
