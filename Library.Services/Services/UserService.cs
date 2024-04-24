using Library.Entities;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Library.Services.Interfaces;
using Library.Services.DTOs;
using System.Text;

namespace Library.Services.Services
{
    public class UserService : IUserService
    {
        private readonly string _token;
        private readonly int _expiresInMinutes = 60;
        private const int _daysToExpire = 7;

        public UserService(IConfiguration configuration)
        {
            _token = configuration.GetSection("Token:Key").Value!;
            _expiresInMinutes = int.Parse(configuration.GetSection("Token:ExpiresInMinutes").Value!.ToString());
        }

        public UserDto Authenticate(AuthenticateRequestDto request)
        {
            // Aquí irá la lógica para autenticar al usuario en una BD
            // Por ahora, simplemente devolvemos un UserDto ficticio
            var userDto = new UserDto { Id = 1, Username = request.Username, Role = "Admin" };

            if (userDto == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_token);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim("id", userDto.Id.ToString()),
                    new Claim("username", userDto.Username.ToString()),
                    new Claim("role", userDto.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_expiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new UserDto
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Role = userDto.Role,
                Token = tokenString
            };
        }

        //public string CreateToken(User user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new (ClaimTypes.Name, user.Username)
        //    };

        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_token));

        //    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(_daysToExpire),
        //        signingCredentials: cred
        //        );

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //    return jwt;
        //}

        //public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using var hmac = new HMACSHA512();
        //    passwordSalt = hmac.Key;
        //    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //}

        //public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using var hmac = new HMACSHA512(passwordSalt);
        //    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    return computedHash.SequenceEqual(passwordHash);
        //}

    }
}
