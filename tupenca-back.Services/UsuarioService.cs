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
    public class UsuarioService : PersonaService<Usuario>
    {
        private readonly IPersonaRepository _db;
        private readonly IConfiguration _configuration;

        public UsuarioService(IPersonaRepository db, IConfiguration configuration) : base(db, configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<Usuario> AuthenticateGoogleUserAsync(string request, string userToken)
        {
            try
            {
                Payload payload = await ValidateAsync(request, new ValidationSettings
                {
                    Audience = new[] { userToken }
                });
                return await GetOrCreateExternalLoginUser("google", payload.Subject, payload.Email, payload.Name);

            }
            catch (Exception e)
            {
                int a = 5;
            }
            return null;

        }

        private async Task<Usuario> GetOrCreateExternalLoginUser(string provider, string key, string email, string name)
        {
            var user = findByEmail(email);
            if (user != null && user.HashedPassword != null)
                //error
                return user;
            if (user == null)
            {
                if(name == null)
                {
                    name = "anonimo";
                }
                user = new Usuario
                {
                    Email = email,
                    UserName = name
                };
                add(user);
            }
            return user;

        }
    }

    

}
