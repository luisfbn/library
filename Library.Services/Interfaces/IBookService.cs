namespace Library.Services
{
    public interface IBookService
    {
        ServiceResult<BookDto> AddBook(BookDto bookDto);
        ServiceResult<bool> DeleteBook(int id);
        ServiceResult<IEnumerable<BookDto>>  GetAllBooks();
    }
}
