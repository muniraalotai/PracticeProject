using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private IHostingEnvironment _hostEnvironment; 

        public BookController(IBookStoreRepository<Book> bookStoreRepository, IBookStoreRepository<Author> authorStoreRepository, IHostingEnvironment hostEnvironment)
        {
            this.bookRepository = bookStoreRepository;
            this.authorRepository = authorStoreRepository;
            this._hostEnvironment = hostEnvironment; 
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
                 try
                 {
                     string fileName = UploadFile(bookModel.File) ?? string.Empty;
                     
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
                         ImageUrl = fileName,
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
                ImageUrl = book.ImageUrl,
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
                string fileName = UploadFile(bookModel.File, bookModel.ImageUrl);
                var author = authorRepository.Find(bookModel.AuthorId);
                Book book = new Book
                {
                    Title = bookModel.Title,
                    Description = bookModel.Description,
                    Author = author,
                    ImageUrl = fileName
                };
                bookRepository.Update(bookModel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(bookModel); 
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
        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(_hostEnvironment.WebRootPath, "photos");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));
                return file.FileName;
            }

            return null;
        }

        string UploadFile(IFormFile file, string imageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(_hostEnvironment.WebRootPath, "photos");
                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, imageUrl);
              //  System.IO.File.Delete(oldPath);
                file.CopyTo(new FileStream(newPath, FileMode.Create));
                return file.FileName;
            }

            return imageUrl;
        }
    }
}
