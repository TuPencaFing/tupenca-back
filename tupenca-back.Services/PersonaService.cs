using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace tupenca_back.Services
{

    public class PersonaService<T> where T : Persona
    {
        private readonly IPersonaRepository _db;
        private readonly IConfiguration _configuration;

        public PersonaService(IPersonaRepository db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public void delete(T usr)
        {
            if (usr != null)
            {
                _db.Remove(usr);
                _db.Save();
            }
        }
        public T? find(int? id)
        {
            return _db.GetFirstOrDefault(u => u.Id == id) as T;
        }


        public T? findByEmail(string email)
        {
            return _db.GetFirstOrDefault(u => u.Email == email) as T;
        }

        public T? findByUserName(string username)
        {
            return _db.GetFirstOrDefault(u => u.UserName == username) as T;
        }

        public IEnumerable<T> get()
        {

            return _db.GetAll().OfType<T>(); ;
        }
        public void edit(T usr)
        {
            if (usr != null)
            {
                _db.Update(usr);
                _db.Save();
            }
        }
        public void add(T usr)
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
        public string CreateToken(Persona user, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
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
