using Microsoft.EntityFrameworkCore;
using PracticeProject.Models;

namespace PracticeProject.Data
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
        }

        public DbSet<Models.Item> Items { get; set; }

        public DbSet<PracticeProject.Models.Author> Author { get; set; }

        public DbSet<PracticeProject.Models.Book> Book { get; set; }
    }
}