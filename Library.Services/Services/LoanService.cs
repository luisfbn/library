using Library.BD;
using Library.Entities;

namespace Library.Services
{
    public class LoanService : ILoanService
    {
        private readonly LibraryDbContext _dbContext;
        const int dueDays = 14; // Due date is set to 14 days

        public LoanService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public ServiceResult<bool> RegisterLoan(int bookId, int readerId)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.Id == bookId && b.IsAvailable);
            var reader = _dbContext.Readers.FirstOrDefault(r => r.Id == readerId);

            if (book == null || reader == null)
            {
                return new ServiceResult<bool> { Success = false, ErrorMessage = "Libro/Lector no encontrado." };
            }

            var loan = new Loan
            {
                BookId = book.Id,
                ReaderId = reader.Id,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(dueDays)
            };

            book.IsAvailable = false;
            _dbContext.Loans.Add(loan);
            _dbContext.SaveChanges();

            return new ServiceResult<bool> { Success = true, Data = true };
        }


        public ServiceResult<bool> ReturnBook(int bookId)
        {
            var loan = _dbContext.Loans.FirstOrDefault(l => l.BookId == bookId && l.ReturnDate == null);

            if (loan == null)
            {
                return new ServiceResult<bool> { Success = false, ErrorMessage = "Préstamo no encontrado o ya fue devuelto." };
            }

            loan.ReturnDate = DateTime.Now;
            _dbContext.SaveChanges();

            var book = _dbContext.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.IsAvailable = true;
                _dbContext.SaveChanges();
            }

            return new ServiceResult<bool> { Success = true, Data = true };
        }


    }
}
