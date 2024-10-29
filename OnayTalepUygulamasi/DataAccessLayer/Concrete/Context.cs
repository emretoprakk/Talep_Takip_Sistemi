using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Department) //Bir kullanıcının bir departmanı vardır
                .WithMany(d => d.Users) //Bir departmanın birden fazla kullanıcısı olabilir 
                .HasForeignKey(u => u.DepartmentId);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.User) //Bir talebin bir kullanıcısı vardır
                .WithMany(u => u.Requests) //Bir kullanıcının birden fazla talebi olabilir 
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.ApprovedBy) //Bir talep, bir kullanıcı tarafından onaylanmış olabilir 
                .WithMany()
                .HasForeignKey(r => r.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .Property(r => r.DepartmentName) // Departman adını ekleme
                .HasMaxLength(100); // Uzunluk sınırlaması

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User) //Bir yorumun bir kullanıcısı vardır 
                .WithMany(u => u.Comments) //Bir kullanıcının birden fazla yorumu olabilir 
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Request) //Bir yorumun bir talebi vardır 
                .WithMany(r => r.Comments) //Bir talebin birden fazla yorumu olabilir 
                .HasForeignKey(c => c.RequestId);

            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Request) //Bir ekin bir talebi vardır
                .WithMany(r => r.Attachments) //Bir talebin birden fazla eki olabilir 
                .HasForeignKey(a => a.RequestId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User) //Bir bildirimin bir kullanıcısı  vardır 
                .WithMany(u => u.Notifications) //Bir kullanıcının birden fazla bildirimi olabilir 
                .HasForeignKey(n => n.UserId);
        }
    }
}

