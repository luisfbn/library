using Microsoft.EntityFrameworkCore;

namespace Library.BD
{
    public class LibraryDbContextFactory
    {
        public LibraryDbContext CreateDbContext(string databaseName = "LibraryDb", bool seedData = false)
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            return new LibraryDbContext(options, this, seedData);
        }

    }

}
