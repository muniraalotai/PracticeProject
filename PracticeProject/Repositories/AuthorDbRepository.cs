using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Models;
using Serilog;

namespace PracticeProject.Repositories
{
    public class AuthorDbRepository : IBookStoreRepository<Author>
    {
        private BookStoreDbContext _dbContext;


        public AuthorDbRepository(BookStoreDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IList<Author> List()
        {
            Log.Information("Get authors list");
            return _dbContext.Authors.ToList();
        }

        public Author Find(int Id)
        {
            Log.Information("Find author by id");
            var author = _dbContext.Authors.SingleOrDefault(a => a.Id == Id);
            return author;
        }

        public void Add(Author entity)
        {
            Log.Information("Add new author");
            _dbContext.Authors.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(int Id, Author entity)
        {
            Log.Information("Update existing author");
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int Id)
        {
            Log.Information("Delete existing author");
            var author = Find(Id);
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }

        public List<Author> Search(string word)
        {
            Log.Information("Search for author by word");
            var result = _dbContext.Authors.Where(a=> a.FullName.Contains(word));
            return result.ToList();
        }
    }
}
