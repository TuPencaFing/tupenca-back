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
    public class FuncionarioService : PersonaService<Funcionario>
    {
        private readonly IPersonaRepository _db;
        private readonly IConfiguration _configuration;

        public FuncionarioService(IPersonaRepository db, IConfiguration configuration) : base(db, configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public string createInviteToken(int id, int pencaId)
        {
            return _db.createInviteToken( id,  pencaId);
        }

        public IEnumerable<Funcionario> getFuncionariosByEmpresa(int empresaId)
        {
            return _db.getFuncionariosByEmpresa(empresaId);
        }

    }
}
