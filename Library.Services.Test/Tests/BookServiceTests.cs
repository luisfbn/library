using Library.BD;

namespace Library.Services.Test
{
    public class BookServiceTests
    {
        private readonly IBookService _bookService;
        private readonly LibraryDbContext _dbContext;

        public BookServiceTests()
        {
            _dbContext = new LibraryDbContextFactory().CreateDbContext("TestLibraryDbForBookTest");
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _bookService = new BookService(_dbContext);
        }


        [Fact]
        public void AddBook_ShouldReturnSuccess()
        {
            // Arrange
            var bookDto = new BookDto
            {
                Title = "Test Book",
                Author = "Test Author"
            };

            // Act
            var result = _bookService.AddBook(bookDto);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(bookDto.Title, result.Data.Title);
            Assert.Equal(bookDto.Author, result.Data.Author);

            // Clean up
            var addedBook = _dbContext.Books.FirstOrDefault(b => b.Id == result.Data.Id);
            if (addedBook != null)
            {
                _dbContext.Books.Remove(addedBook);
                _dbContext.SaveChanges();
            }
        }

        [Fact]
        public void DeleteBook_ExistingBook_ShouldReturnSuccess()
        {
            // Arrange
            var bookDto = new BookDto
            {
                Title = "Test Book",
                Author = "Test Author"
            };
            var addedBook = _dbContext.Books.Add(new Entities.Book { Title = bookDto.Title, Author = bookDto.Author, IsAvailable = true }).Entity;
            _dbContext.SaveChanges();

            // Act
            var result = _bookService.DeleteBook(addedBook.Id);
            _dbContext.SaveChanges();

            // Assert
            Assert.True(result.Success);
            Assert.Null(_dbContext.Books.Find(addedBook.Id));
        }

        [Fact]
        public void DeleteBook_NonExistingBook_ShouldReturnFailure()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _bookService.DeleteBook(nonExistingId);
            _dbContext.SaveChanges();

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(result.ErrorMessage);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            // Arrange
            var bookDto1 = new BookDto { Title = "Book 1", Author = "Author 1" };
            var bookDto2 = new BookDto { Title = "Book 2", Author = "Author 2" };

            _bookService.AddBook(bookDto1);
            _bookService.AddBook(bookDto2);

            // Act
            var result = _bookService.GetAllBooks();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }


    }

}
