using Newtonsoft.Json.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
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

        //public List<int> getUsersWith1HourOfEventoEnd()
        //{

        //    var today = DateTime.Now;
        //    //return _appDbContext.Eventos
        //    // .Where(e => (e.FechaInicial - DateTime.Now) <= TimeSpan.FromHours(24)).SelectMany(e => e.Campeonatos).SelectMany(p => p.Pencas).SelectMany(p => p.UsuariosPencas).Select(p => p.Usuario).Where(pc => !_appDbContext.Predicciones
        //    //        .Any(up => up.EventoId ==  && up.UsuarioId == pc.Id)).ToList();
        //    //return from Evento x in _appDbContext.Usuarios.Select(p => x.)
        //      //select new Customer()
        //      //{
        //      //    CustomerID = x.CustomerID,
        //      //    FirstName = x.FirstName,
        //      //    LastName = x.LastName,
        //      //    Gender = x.Gender,
        //      //    BirthMonth = x.BirthMonth,
        //      //    TotalPurchases = context.PurchaseOrders
        //      //                          .Where(po => po.CustomerId == x.CustomerId)
        //      //                          .Count()
        //      //};

        //}

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

        public string createResetToken(int id)
        {

            string token = RandomString(16);

            var userToken = new PersonaResetPassword
            {
                Token = token,
                PersonaId = id,
            };
            if (_appDbContext.PersonaResetPassword.Any(x => x.Token == token))
            {
                _appDbContext.PersonaResetPassword.Update(userToken);
            }
            else
            {
                _appDbContext.PersonaResetPassword.Add(userToken);
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

        public PersonaResetPassword getPersonaResetPassword(string access_token)
        {
            return _appDbContext.PersonaResetPassword.Find(access_token);
        }
        // Generates a random string with a given size.    
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);


            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  
            Random _random = new Random();

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }


}
