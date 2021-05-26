using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Models;
using PracticeProject.Repositories;

namespace PracticeProject.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;

        public BookController(IBookStoreRepository<Book> bookStoreRepository)
        {
            this.bookRepository = bookStoreRepository;
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
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            try
            {
                bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(book);   
            }
        }

        // GET: Book/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Description")] Book book)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(book); 
            }
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
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
