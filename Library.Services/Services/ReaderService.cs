using Library.BD;
using Library.Services.DTOs;
using Library.Services.Interfaces;

namespace Library.Services.Services
{
    public class ReaderService : IReaderService
    {
        private readonly LibraryDbContext _dbContext;

        public ReaderService(LibraryDbContext dbContext)
        {
            _dbContext= dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public ServiceResult<IEnumerable<ReaderDto>> GetAll()
        {
            var list = _dbContext.Readers
                .Where(x => x.IsActive)
                .Select(b => new ReaderDto
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .OrderBy(o => o.Name)
                .ToList();

            return new ServiceResult<IEnumerable<ReaderDto>> { 
                Success = true, 
                Data = list, 
                ErrorMessage = "" 
            };
        }

    }
}
