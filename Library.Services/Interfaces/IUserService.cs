using Library.Services.DTOs;

namespace Library.Services.Interfaces
{
    public interface IUserService
    {
        UserDto Authenticate(AuthenticateRequestDto request);

        //string CreateToken(User user);
        //void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        //bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
