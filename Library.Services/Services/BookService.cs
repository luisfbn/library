using Library.Entities;
using Library.BD;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            _dbContext= dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public ServiceResult<BookDto> AddBook(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                IsAvailable = true
            };

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            bookDto.Id = book.Id;

            return new ServiceResult<BookDto>
            {
                Success = true,
                Data = bookDto
            };
        }

        public ServiceResult<bool> DeleteBook(int id)
        {
            var book = _dbContext.Books.Find(id);

            if (book == null)
            {
                return new ServiceResult<bool>
                {
                    Success = false,
                    Data = false,
                    ErrorMessage = "Libro no encontrado"
                };
            }

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

            return new ServiceResult<bool>
            {
                Success = true,
                Data = true
            };
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var list = _dbContext
                .Books
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    IsAvailable = b.IsAvailable
                })
                .ToList();

            return list;
        }

    }

}
