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

        public IEnumerable<ReaderDto> GetAll()
        {
            var list = _dbContext.Readers
                .Where(x => x.IsActive)
                .Select(b => new ReaderDto
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToList();

            return list;
        }
    }
}
