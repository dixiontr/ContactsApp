using ContactsApp.ContactService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContactsApp.ContactService.Context
{

    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options){ }

        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ContactInformation>()
                .Property(d => d.InformationType)
                .HasConversion(new EnumToStringConverter<InformationType>());
            base.OnModelCreating(modelBuilder);
        }
    }

}