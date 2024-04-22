
using Library.Services.DTOs;

namespace Library.Services.Interfaces
{
    public interface IReaderService
    {
        IEnumerable<ReaderDto> GetAll();
    }
}
