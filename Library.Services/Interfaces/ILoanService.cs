namespace Library.Services
{
    public interface ILoanService
    {
        ServiceResult<bool> RegisterLoan(int bookId, int readerId);
        ServiceResult<bool> ReturnBook(int bookId);
    }
}
