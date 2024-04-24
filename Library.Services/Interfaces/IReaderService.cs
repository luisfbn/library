using Library.Services.DTOs;

namespace Library.Services.Interfaces
{
    public interface IReaderService
    {
        ServiceResult<IEnumerable<ReaderDto>> GetAll();
    }
}
