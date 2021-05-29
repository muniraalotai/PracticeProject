using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Models;
using PracticeProject.Repositories;
using PracticeProject.ViewModels;

namespace PracticeProject.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IBookStoreRepository<Author> authorRepository;

        public BookController(IBookStoreRepository<Book> bookStoreRepository, IBookStoreRepository<Author> authorStoreRepository)
        {
            this.bookRepository = bookStoreRepository;
            this.authorRepository = authorStoreRepository;
        }

        // GET: Book
        public IActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: Book/Details/5
        public IActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View(GetAllAuthorsModel());
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookAuthorViewModel bookModel)
        {
            if (ModelState.IsValid)
            {
                 try {
                    if (bookModel.AuthorId == -1) {
                        ViewBag.Message = "Please select an author from the list";
                        return View(GetAllAuthorsModel());
                    }
                    var author = authorRepository.Find(bookModel.AuthorId);
                    Book book = new Book
                    {
                        Id = bookModel.BookId,
                        Title = bookModel.Title,
                        Description = bookModel.Description,
                        Author = author
                    };
                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                 }catch
                 {
                    return View();   
                 }
            }
            ModelState.AddModelError("", "You have to fill all required fields!");
            return View(GetAllAuthorsModel());
        }

        // GET: Book/Edit/5
        public IActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var model = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepository.List().ToList()
            };
            return View(model);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookAuthorViewModel bookModel)
        {
            try
            {
                var author = authorRepository.Find(bookModel.AuthorId);
                Book book = new Book
                {
                    Title = bookModel.Title,
                    Description = bookModel.Description,
                    Author = author
                };
                bookRepository.Update(bookModel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(); 
            }
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(); 
            }
        }
        
        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "Please select an author" });
            return authors;
        }
        
        BookAuthorViewModel GetAllAuthorsModel()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return model;
        }
    }
}
