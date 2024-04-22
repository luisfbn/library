using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.BD
{
    public class LibraryDbContext : DbContext
    {
        private readonly LibraryDbContextFactory _dbContextFactory;
        private bool _seedData = true;

        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Loan> Loans { get; set; }



        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options, LibraryDbContextFactory dbContextFactory, bool seedData = true) : base(options)
        {
            _dbContextFactory = dbContextFactory;
            _seedData = seedData;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // optionsBuilder.UseInMemoryDatabase("LibraryDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!_seedData) return;

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 2,
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt, David Thomas",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 3,
                    Title = "Design Patterns",
                    Author = "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 4,
                    Title = "Code Complete",
                    Author = "Steve McConnell",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 5,
                    Title = "Clean Architecture",
                    Author = "Robert C. Martin",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 6,
                    Title = "Refactoring",
                    Author = "Martin Fowler",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 7,
                    Title = "Domain-Driven Design",
                    Author = "Eric Evans",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 8,
                    Title = "Programming Pearls",
                    Author = "Jon Bentley",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 9,
                    Title = "The Mythical Man-Month",
                    Author = "Frederick P. Brooks Jr.",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 10,
                    Title = "Head First Design Patterns",
                    Author = "Eric Freeman and Elisabeth Robson",
                    IsAvailable = true
                }
            );

            modelBuilder.Entity<Reader>().HasData(
                new Reader { Id = 1, Name = "Juan", IsActive = true },
                new Reader { Id = 2, Name = "Pedro", IsActive = true },
                new Reader { Id = 3, Name = "Ana", IsActive = true },
                new Reader { Id = 4, Name = "Pablo", IsActive = true },
                new Reader { Id = 5, Name = "Silvia", IsActive = true }
            );
        }

    }

}
