using Microsoft.EntityFrameworkCore;

namespace PracticeProject.Data
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
        }

        public DbSet<Models.Item> Items { get; set; }
    }
}