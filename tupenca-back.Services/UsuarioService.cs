using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Xml.Linq;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using tupenca_back.Services.Exceptions;
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

        public int getPencaIdFromToken(string access_token)
        {
            var inviteToken = _db.getUserInviteToken(access_token);
            if (inviteToken == null)
            {
                throw new NotFoundException("El Token no existe");
            }
            else
            {
                return inviteToken.PencaId;

            }
        }

        public void RemoveUserToken(string access_token)
        {
            var usertoken = _db.getUserInviteToken(access_token);

            if (usertoken == null)
                throw new NotFoundException("El token no existe");

            _db.RemoveUserToken(usertoken);
            _db.Save();
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

        public int GetCantUsuarios()
        {
            return _db.getCantUsuarios();
        }
        public string createResetToken(int id)
        {
            return _db.createResetToken(id);
        }

        public IEnumerable<Usuario> getUsuarios()
        {
            return _db.GetAll().OfType<Usuario>().ToList();
        }
        public int getPersonaIdFromToken(string access_token)
        {
            var token = _db.getPersonaResetPassword(access_token);
            if (token == null)
            {
                throw new NotFoundException("El Token no existe");
            }
            else
            {
                return token.PersonaId;

            }
        }

        public void UpdateUsuario(Usuario user)
        {
            if (user != null)
            {
                _db.Update(user);
                _db.Save();
            }
        }

        public Usuario getUsuario(int id)
        {
            return _db.GetUsuario(db => db.Id == id);
        }
    }

    

}
