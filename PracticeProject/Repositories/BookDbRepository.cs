using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using PracticeProject.Data;
using PracticeProject.Models;

namespace PracticeProject.Repositories
{
    public class BookDbRepository: IBookStoreRepository<Book>
    {
        public BookStoreDbContext _dbContext;
        
        public BookDbRepository(BookStoreDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IList<Book> List()
        {
            return _dbContext.Books.Include(a => a.Author).ToList();
        }

        public Book Find(int Id)
        {
            var book = _dbContext.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == Id);
            return book;
        }

        public void Add(Book entity)
        {
            _dbContext.Books.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(int Id, Book entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int Id)
        {
            var book = Find(Id);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

        public List<Book> Search(string word)
        {
            var result = _dbContext.Books.Include(a => a.Author)
                .Where(b => b.Title.Contains(word)
                            ||b.Description.Contains(word)
                            ||b.Author.FullName.Contains(word));
            return result.ToList();
        }
    }
}