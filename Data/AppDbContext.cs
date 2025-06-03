using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;

namespace StudentManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Student>()
                .HasData(
                    new Student
                    {
                        StudentNumber = "222997496",
                        FirstName = "Dennis",
                        LastName = "Daniels",
                        EmailAddress = "dennis@daniels.com",
                        DateOfBirth = new DateTime(2002, 11, 06, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997497",
                        FirstName = "Sarah",
                        LastName = "Lee",
                        EmailAddress = "sarah.lee@email.com",
                        DateOfBirth = new DateTime(2003, 02, 14, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997498",
                        FirstName = "Michael",
                        LastName = "Jordan",
                        EmailAddress = "mjordan@email.com",
                        DateOfBirth = new DateTime(2001, 06, 10, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997499",
                        FirstName = "Emily",
                        LastName = "Watson",
                        EmailAddress = "emily.watson@email.com",
                        DateOfBirth = new DateTime(2004, 03, 22, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997500",
                        FirstName = "James",
                        LastName = "Smith",
                        EmailAddress = "james.smith@email.com",
                        DateOfBirth = new DateTime(2000, 12, 01, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997501",
                        FirstName = "Olivia",
                        LastName = "Brown",
                        EmailAddress = "olivia.brown@email.com",
                        DateOfBirth = new DateTime(2003, 09, 18, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997502",
                        FirstName = "William",
                        LastName = "Davis",
                        EmailAddress = "william.davis@email.com",
                        DateOfBirth = new DateTime(2002, 07, 11, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997503",
                        FirstName = "Ava",
                        LastName = "Taylor",
                        EmailAddress = "ava.taylor@email.com",
                        DateOfBirth = new DateTime(2001, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997504",
                        FirstName = "Benjamin",
                        LastName = "Wilson",
                        EmailAddress = "benjamin.wilson@email.com",
                        DateOfBirth = new DateTime(2003, 01, 15, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997505",
                        FirstName = "Charlotte",
                        LastName = "Moore",
                        EmailAddress = "charlotte.moore@email.com",
                        DateOfBirth = new DateTime(2002, 05, 30, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997506",
                        FirstName = "Lucas",
                        LastName = "Jackson",
                        EmailAddress = "lucas.jackson@email.com",
                        DateOfBirth = new DateTime(2001, 04, 12, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997507",
                        FirstName = "Amelia",
                        LastName = "Martin",
                        EmailAddress = "amelia.martin@email.com",
                        DateOfBirth = new DateTime(2004, 08, 08, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997508",
                        FirstName = "Henry",
                        LastName = "White",
                        EmailAddress = "henry.white@email.com",
                        DateOfBirth = new DateTime(2000, 03, 03, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997509",
                        FirstName = "Isabella",
                        LastName = "Harris",
                        EmailAddress = "isabella.harris@email.com",
                        DateOfBirth = new DateTime(2003, 06, 17, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997510",
                        FirstName = "Daniel",
                        LastName = "Thompson",
                        EmailAddress = "daniel.thompson@email.com",
                        DateOfBirth = new DateTime(2001, 09, 21, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997511",
                        FirstName = "Mia",
                        LastName = "Garcia",
                        EmailAddress = "mia.garcia@email.com",
                        DateOfBirth = new DateTime(2002, 12, 09, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997512",
                        FirstName = "Logan",
                        LastName = "Martinez",
                        EmailAddress = "logan.martinez@email.com",
                        DateOfBirth = new DateTime(2003, 07, 27, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997513",
                        FirstName = "Sophia",
                        LastName = "Robinson",
                        EmailAddress = "sophia.robinson@email.com",
                        DateOfBirth = new DateTime(2000, 10, 02, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997514",
                        FirstName = "Alexander",
                        LastName = "Clark",
                        EmailAddress = "alex.clark@email.com",
                        DateOfBirth = new DateTime(2001, 01, 29, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997515",
                        FirstName = "Grace",
                        LastName = "Lewis",
                        EmailAddress = "grace.lewis@email.com",
                        DateOfBirth = new DateTime(2004, 02, 04, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new Student
                    {
                        StudentNumber = "222997516",
                        FirstName = "Ethan",
                        LastName = "Walker",
                        EmailAddress = "ethan.walker@email.com",
                        DateOfBirth = new DateTime(2002, 06, 19, 0, 0, 0, DateTimeKind.Utc),
                    }
                );
        }
    }
}
