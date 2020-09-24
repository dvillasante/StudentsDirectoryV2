using Business.Helpers;
using Business.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Business
{
    public class UserManager : IUserManager
    {

        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "User", LastName = "Administrator", Username = "admin", Password = "admin", Role = Role.Admin },
            new User { Id = 2, FirstName = "User", LastName = "Regular", Username = "user", Password = "user", Role = Role.User }
        };

        private readonly AppSettings _appSettings;

        public UserManager(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return null;

            //jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);

            return user;
        }
    }
}
