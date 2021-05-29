using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PracticeProject.Models;

namespace PracticeProject.Repositories
{
    public class BookRepository: IBookStoreRepository<Book>
    {
        private List<Book> books;
        
        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id=1, Title = "Team leadership",
                    Description = "Team leadership", 
                    ImageUrl = "copy.jpg",
                    Author = new Author {Id = 1}
                },
                new Book
                {
                    Id=2, Title = "Project management",
                    Description = "Project management", 
                    ImageUrl = "palm.jpg",
                    Author = new Author {Id = 2}
                }, 
                new Book
                {
                    Id=3, Title = "Software engineering",
                    Description = "Software engineering", 
                    ImageUrl = "Riyadh.jpg",
                    Author = new Author {Id = 3}
                },
            };
        }

        public IList<Book> List()
        {
            return books;
        }

        public Book Find(int Id)
        {
            var book = books.SingleOrDefault(b => b.Id == Id);
            return book;
        }

        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1;
            books.Add(entity);
        }

        public void Update(int Id, Book entity)
        {
            var book = Find(Id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.ImageUrl = entity.ImageUrl;
            book.Author = entity.Author;
           
        }

        public void Delete(int Id)
        {
            var book = Find(Id);
            books.Remove(book);
        }
    }
}