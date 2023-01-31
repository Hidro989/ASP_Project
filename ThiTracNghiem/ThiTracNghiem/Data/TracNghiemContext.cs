using Microsoft.EntityFrameworkCore;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.Data
{
    public class TracNghiemContext : DbContext
    {
        public TracNghiemContext(DbContextOptions<TracNghiemContext> options) : base(options) { }

        public DbSet<MonThi> DsMonThi { get; set; }
        public DbSet<DeThi> DsDeThi { get; set; }
        public DbSet<CauHoi> DsCauHoi { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<MaThi> DsMaThi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonThi>().ToTable("MonThi");
            modelBuilder.Entity<DeThi>().ToTable("DeThi");
            modelBuilder.Entity<CauHoi>().ToTable("CauHoi");
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<MaThi>().ToTable("MaThi");
        }

    }
}
