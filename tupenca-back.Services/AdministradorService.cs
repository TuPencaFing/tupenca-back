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
    public class AdministradorService : PersonaService<Administrador>
    {
        private readonly IPersonaRepository _db;
        private readonly IConfiguration _configuration;
        private readonly EmpresaService _empresaService;
        private readonly UsuarioService _usuarioService;
        private readonly PencaService _pencaService;


        public AdministradorService(IPersonaRepository db,
                                    IConfiguration configuration,
                                    EmpresaService empresaService,
                                    UsuarioService usuarioService,
                                    PencaService pencaService) : base(db, configuration)
        {
            _db = db;
            _configuration = configuration;
            _empresaService = empresaService;
            _usuarioService = usuarioService;
            _pencaService = pencaService;
        }



        public Metrica GetMetrica()
        {
            Metrica metrica = new Metrica();

            var ganancia = _pencaService.GananciasPencasCompartidas() + _empresaService.GetGananciasPorPlan();

            metrica.cantEmpresasRegistradas = _empresaService.CantEmpresas();
            metrica.cantUsuariosRegistrados = _usuarioService.GetCantUsuarios();
            metrica.cantPencasActivas = _pencaService.CantPencasActivas();
            metrica.ganancias = ganancia;

            return metrica;
        }
    }
}
