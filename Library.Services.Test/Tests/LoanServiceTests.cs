using Library.BD;
using Library.Entities;

namespace Library.Services.Test
{
    public class LoanServiceTests
    {
        private readonly ILoanService _loanService;
        private readonly LibraryDbContext _dbContext;

        public LoanServiceTests()
        {
            _dbContext = new LibraryDbContextFactory().CreateDbContext("TestLibraryDbForLoanTest");
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _loanService = new LoanService(_dbContext);
        }

        [Fact]
        public void RegisterLoan_ShouldReturnSuccess()
        {
            // Arrange
            var book = new Book { Title = "Test Book", Author = "Test Author", IsAvailable = true };
            var reader = new Reader { Name = "Test Reader", IsActive = true };

            _dbContext.Books.Add(book);
            _dbContext.Readers.Add(reader);
            _dbContext.SaveChanges();

            // Act
            var result = _loanService.RegisterLoan(book.Id, reader.Id);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(_dbContext.Loans.FirstOrDefault(l => l.BookId == book.Id && l.ReaderId == reader.Id));
            Assert.False(_dbContext.Books.FirstOrDefault(b => b.Id == book.Id)?.IsAvailable);

            // Clean up
            var addedBook = _dbContext.Books.First(b => b.Id == book.Id);
            var addedReader = _dbContext.Readers.First(b => b.Id == reader.Id);

            _dbContext.Books.Remove(addedBook);
            _dbContext.Readers.Remove(addedReader);
            _dbContext.SaveChanges();
        }


        [Fact]
        public void RegisterLoan_NonExistingBook_ShouldReturnFailure()
        {
            // Arrange
            var nonExistingId = 999;
            var reader = new Reader { Name = "Test Reader", IsActive = true };

            _dbContext.Readers.Add(reader);
            _dbContext.SaveChanges();

            // Act
            var result = _loanService.RegisterLoan(nonExistingId, reader.Id);

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(result.ErrorMessage);

        }


        [Fact]
        public void ReturnBook_ExistingBookOnLoan_ShouldReturnSuccess()
        {
            // Arrange
            var book = new Book { Title = "Test Book 3", Author = "Test Author", IsAvailable = true };
            var reader = new Reader { Name = "Test Reader", IsActive = true };

            _dbContext.Books.Add(book);
            _dbContext.Readers.Add(reader);
            _dbContext.SaveChanges();

            var resultRegister = _loanService.RegisterLoan(book.Id, reader.Id);

            // Act
            var result = _loanService.ReturnBook(book.Id);

            // Assert
            Assert.True(result.Success);
            Assert.True(result.Data);

            // Clean up
            var addedBook = _dbContext.Books.First(b => b.Id == book.Id);
            var addedReader = _dbContext.Readers.First(b => b.Id == reader.Id);

            _dbContext.Books.Remove(addedBook);
            _dbContext.Readers.Remove(addedReader);
            _dbContext.SaveChanges();
        }

    }

}
