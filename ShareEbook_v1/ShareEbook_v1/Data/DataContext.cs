using Microsoft.EntityFrameworkCore;
using ShareEbook_v1.Models;

namespace ShareEbook_v1.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Notifi> Notifis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Notifi>().ToTable("Notifi");

            
        }
    }
}
