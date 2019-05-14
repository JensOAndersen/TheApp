using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TheApp.Api.Entities;
using TheApp.Api.Helpers;

namespace TheApp.Api.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {
        //replace this part with a repository containing users
        private List<User> _users = new List<User>()
        {
            new User {Id = -1, Firstname = "test", Lastname = "test", Username = "test", Password = "test"}
        };

        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            //this is where i'd call a repository with the users. The passwords needs to be hashed first
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null; //consider doing something else, seems a bit silly that user security is based on developer wisdom

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Select(x =>
            {
                x.Password = null;
                return x;
            });
        }
    }
}