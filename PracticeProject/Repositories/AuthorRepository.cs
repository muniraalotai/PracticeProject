using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PracticeProject.Models;

namespace PracticeProject.Repositories
{
    public class AuthorRepository:IBookStoreRepository<Author>
    {
        private IList<Author> authors;

        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author
                {
                    Id = 1, FullName = "Ahmed",
                },
                new Author
                {
                    Id = 2, FullName = "Sami",
                },
                new Author
                {
                    Id = 3, FullName = "Khalid",
                }
            };
        }

        public IList<Author> List()
        {
            return authors;
        }

        public Author Find(int Id)
        {
            var author = authors.SingleOrDefault(a => a.Id == Id);
            return author;
        }

        public void Add(Author entity)
        {
            entity.Id = authors.Max(b => b.Id) + 1;
            authors.Add(entity);
        }

        public void Update(int Id, Author entity)
        {
            var author = Find(Id);
            author.FullName = entity.FullName;
        }

        public void Delete(int Id)
        {
            var author = Find(Id);
            authors.Remove(author);
        }
    }
}