﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class UserService
    {
        private readonly IUserRepository _db;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository db,IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public void deleteUser(User usr)
        {
            if (usr != null)
            {
                _db.Remove(usr);
                _db.Save();
            }
        }
        public User? findUser(int? id)
        {
            return _db.GetFirstOrDefault(u=> u.Id == id);
        }
        public User? findUserByEmail(string email)
        {
            return _db.GetFirstOrDefault(u => u.Email == email);
        }

        public User? findUserByUserName(string username)
        {
            return _db.GetFirstOrDefault(u => u.UserName == username);
        }

        public IEnumerable<User> getUsers()
        {

            return _db.GetAll();
        }
        public void editUser(User usr)
        {
            if (usr != null)
            {
                _db.Update(usr);
                _db.Save();
            }
        }
        public void addUser(User usr)
        {
            if (usr != null)
            {
                _db.Add(usr);
                _db.Save();
            }
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
