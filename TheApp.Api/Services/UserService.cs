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
using TheApp.Api.Model;

namespace TheApp.Api.Services
{
    public interface IUserService
    {
        UserDTOOut Authenticate(string username, string password);
        IEnumerable<UserDTOOut> GetAll();
        UserDTOOut GetById(int Id);
        DBUser Create(DBUser user, string password);
        void Update(UserDTOIn user, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public UserDTOOut Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username); //doesnt check for PW, make sure in creation that usernames are unique

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return new UserDTOOut
            {
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Id = user.Id
            };
        }

        public DBUser Create(DBUser user, string password)
        {
            if (string.IsNullOrEmpty(password))
                return null;

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" does already exist");

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<UserDTOOut> GetAll()
        {
            return _context.Users.Select(x =>
                new UserDTOOut
                {
                    Firstname = x.Firstname,
                    Username = x.Username,
                    Id = x.Id,
                    Lastname = x.Lastname
                });
        }

        public UserDTOOut GetById(int Id)
        {
            DBUser user = _context.Users.SingleOrDefault(x => x.Id == Id);

            return new UserDTOOut
            {
                Firstname = user.Firstname,
                Id = user.Id,
                Lastname = user.Lastname,
                Username = user.Username
            };

        }

        public void Update(UserDTOIn userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.Username != user.Username)
            {   //this is used in case the user is changing his username to an existing username
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException($"Username {userParam.Username} is already taken");
            }

            user.Firstname = userParam.Firstname;
            user.Lastname = userParam.Lastname;
            user.Username = userParam.Username;

            if (string.IsNullOrEmpty(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password must not be null");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("value cannot be empty or whitespace", "password");
            if (storedHash.Length != 64) throw new ArgumentException("invalid length of password hash, 64 bit is required", "passwordhash");
            if (storedSalt.Length != 128) throw new ArgumentException("invalid length of password salt, 128 bit is required", "passwordsalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}