using Infraestructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data
{
    public class ContactContext : DbContext
    {
        internal DbSet<Contact> Contacts { get; set; }
        public ContactContext(DbContextOptions options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<Contact>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<Phone>()
                .HasIndex(u => u.Number)
                .IsUnique();
        }
    }
}