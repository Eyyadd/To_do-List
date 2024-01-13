using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Entities;

namespace To_Do_List
{
    public class ApplicationDbContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-A7K294K\\SQLEXPRESS;Initial Catalog=To_do List ;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(pk => pk.userId);
            modelBuilder.Entity<User>().Property(FN => FN.FirstName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<User>().Property(LN => LN.LastName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<User>().Property(M => M.Mail).HasMaxLength(150);
            modelBuilder.Entity<User>().HasMany(p => p.Tasks).WithOne(t => t.user).HasForeignKey(t => t.userId).HasPrincipalKey(u => u.userId);
            modelBuilder.Entity<Tasks>().HasKey(pk => pk.TaskId);
            modelBuilder.Entity<User>().HasIndex(p => new {p.Mail,p.Password});
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
    }
}
