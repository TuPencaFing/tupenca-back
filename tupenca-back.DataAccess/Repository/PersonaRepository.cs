using Newtonsoft.Json.Linq;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;


namespace tupenca_back.DataAccess.Repository
{
    public class PersonaRepository : Repository<Persona>, IPersonaRepository
    {
        private AppDbContext _appDbContext;
        public PersonaRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public string createInviteToken(int id, int pencaId)
        {
            //var idQuery =
            //from func in _appDbContext.Funcionarios
            //where func.I d == id
            //select func;
            //Funcionario func = _appDbContext.Funcionarios.Include(p=>p.Empresa).FirstOrDefault(f => f.Id == id);
            //var emp =  _appDbContext.Empresas.Where(p => p.Id == func.EmpresaId).Include(p => p.Pencas).FirstOrDefault();

            //Empresa emp = _appDbContext.Empresas.FirstOrDefault(f => f.Id == func.EmpresaId);
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = BitConverter.GetBytes(pencaId);

            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            var userToken = new UserInviteToken
            {
                Token = token,
                PencaId = pencaId,
            };
            if (_appDbContext.UserInviteTokens.Any(x => x.Token == token))
            {
                _appDbContext.UserInviteTokens.Update(userToken);
            }
            else
            {
                _appDbContext.UserInviteTokens.Add(userToken);
            }
            _appDbContext.SaveChanges();


            //var pencas = emp.Pencas;
            //var je = (from d in pencas
            //     where d.Id == pencaId
            //     select d);
            return token;
            //Empresa empresa = _appDbContext.Empresas.Select(e => e.Funcionarios.)
            //var query = _appDbContext.Funcionarios.Where(f => f.Empresa.Pencas.Where(p => p.Id == pencaId))
        }

        public UserInviteToken getUserInviteToken(string access_token)
        {
            return _appDbContext.UserInviteTokens.Find(access_token);
        }

        public void RemoveUserToken(UserInviteToken usertoken)
        {
            _appDbContext.UserInviteTokens.Remove(usertoken);
        }
	public IEnumerable<Funcionario> getFuncionariosByEmpresa(int empresaId)
        {
            return _appDbContext.Funcionarios.Where(f => f.EmpresaId == empresaId).ToList();
        }

        public int getCantUsuarios()
        {
            return _appDbContext.Usuarios.Count();
        }

        public List<int> getUsersWithPredictionOfEvento(int eventoId)
        {
            return _appDbContext.Predicciones
             .Where(p => p.EventoId == eventoId).Select(p => p.UsuarioId).ToList();

        }

        public List<string> getUsersNotificationTokens(List<int> usrersId)
        {
            return _appDbContext.NotificationUserDeviceIds
                               .Where(t => usrersId.Contains(t.Id)).Select(n => n.deviceId).ToList();
        }

        public void createNotificationDeviceId(int userId, string deviceId)
        {
            var userDeviceId = new NotificationUserDeviceId
            {
                Id = userId,
                deviceId = deviceId,
            };
            if (_appDbContext.NotificationUserDeviceIds.Any(x => x.Id == userId))
            {
                _appDbContext.NotificationUserDeviceIds.Update(userDeviceId);
            }
            else
            {
                _appDbContext.NotificationUserDeviceIds.Add(userDeviceId);
            }
            _appDbContext.SaveChanges();
        }
    }
}
