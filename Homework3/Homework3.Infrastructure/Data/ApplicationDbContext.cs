using Homework3.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework3.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data - 15 students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Samanta", LastName = "William", BirthDate = new DateTime(2008, 5, 15), Email = "samanta.william@email.com", PhoneNumber = "+1234567890", ParentName = "Mana William", City = "Jakarta", Grade = "VII A", StudentId = "#123456789" },
                new Student { Id = 2, FirstName = "Tony", LastName = "Soap", BirthDate = new DateTime(2007, 8, 22), Email = "tony.soap@email.com", PhoneNumber = "+1234567891", ParentName = "James Soap", City = "Jakarta", Grade = "VII B", StudentId = "#123456789" },
                new Student { Id = 3, FirstName = "Karen", LastName = "Hope", BirthDate = new DateTime(2008, 3, 10), Email = "karen.hope@email.com", PhoneNumber = "+1234567892", ParentName = "Justin Hope", City = "Jakarta", Grade = "VII C", StudentId = "#123456789" },
                new Student { Id = 4, FirstName = "Jordan", LastName = "Nico", BirthDate = new DateTime(2009, 1, 5), Email = "jordan.nico@email.com", PhoneNumber = "+1234567893", ParentName = "Amanda Nico", City = "Jakarta", Grade = "VII A", StudentId = "#123456789" },
                new Student { Id = 5, FirstName = "Nadila", LastName = "Adja", BirthDate = new DateTime(2008, 11, 30), Email = "nadila.adja@email.com", PhoneNumber = "+1234567894", ParentName = "Jack Adja", City = "Jakarta", Grade = "VII A", StudentId = "#123456789" },
                new Student { Id = 6, FirstName = "Johnny", LastName = "Ahmad", BirthDate = new DateTime(2007, 6, 18), Email = "johnny.ahmad@email.com", PhoneNumber = "+1234567895", ParentName = "Danny Ahmad", City = "Jakarta", Grade = "VII A", StudentId = "#123456789" },
                new Student { Id = 7, FirstName = "Alice", LastName = "Cooper", BirthDate = new DateTime(2008, 9, 25), Email = "alice.cooper@email.com", PhoneNumber = "+1234567896", ParentName = "David Cooper", City = "Bandung", Grade = "VII B", StudentId = "#123456790" },
                new Student { Id = 8, FirstName = "Michael", LastName = "Johnson", BirthDate = new DateTime(2007, 12, 8), Email = "michael.johnson@email.com", PhoneNumber = "+1234567897", ParentName = "Sarah Johnson", City = "Surabaya", Grade = "VII C", StudentId = "#123456791" },
                new Student { Id = 9, FirstName = "Emily", LastName = "Brown", BirthDate = new DateTime(2008, 4, 14), Email = "emily.brown@email.com", PhoneNumber = "+1234567898", ParentName = "Robert Brown", City = "Jakarta", Grade = "VII A", StudentId = "#123456792" },
                new Student { Id = 10, FirstName = "David", LastName = "Martinez", BirthDate = new DateTime(2009, 2, 20), Email = "david.martinez@email.com", PhoneNumber = "+1234567899", ParentName = "Lisa Martinez", City = "Medan", Grade = "VII B", StudentId = "#123456793" },
                new Student { Id = 11, FirstName = "Sarah", LastName = "Garcia", BirthDate = new DateTime(2008, 7, 11), Email = "sarah.garcia@email.com", PhoneNumber = "+1234567800", ParentName = "Carlos Garcia", City = "Jakarta", Grade = "VII C", StudentId = "#123456794" },
                new Student { Id = 12, FirstName = "James", LastName = "Wilson", BirthDate = new DateTime(2007, 10, 3), Email = "james.wilson@email.com", PhoneNumber = "+1234567801", ParentName = "Patricia Wilson", City = "Semarang", Grade = "VII A", StudentId = "#123456795" },
                new Student { Id = 13, FirstName = "Jessica", LastName = "Anderson", BirthDate = new DateTime(2008, 8, 28), Email = "jessica.anderson@email.com", PhoneNumber = "+1234567802", ParentName = "Mark Anderson", City = "Jakarta", Grade = "VII B", StudentId = "#123456796" },
                new Student { Id = 14, FirstName = "Daniel", LastName = "Taylor", BirthDate = new DateTime(2009, 3, 17), Email = "daniel.taylor@email.com", PhoneNumber = "+1234567803", ParentName = "Nancy Taylor", City = "Yogyakarta", Grade = "VII C", StudentId = "#123456797" },
                new Student { Id = 15, FirstName = "Laura", LastName = "Thomas", BirthDate = new DateTime(2008, 6, 9), Email = "laura.thomas@email.com", PhoneNumber = "+1234567804", ParentName = "Steven Thomas", City = "Jakarta", Grade = "VII A", StudentId = "#123456798" }
            );
        }
    }
}