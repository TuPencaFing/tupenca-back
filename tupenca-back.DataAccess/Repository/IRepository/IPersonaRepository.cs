using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IPersonaRepository : IRepository<Persona>
    {
        string createInviteToken(int id, int pencaId);
        void createNotificationDeviceId(int userId, string deviceId);
        UserInviteToken getUserInviteToken(string access_token);
        PersonaResetPassword getPersonaResetPassword(string access_token);

        List<string> getUsersNotificationTokens(List<int> usrersId);
        List<int> getUsersWithPredictionOfEvento(int eventoId);
        void RemoveUserToken(UserInviteToken usertoken);
        void Save();
        IEnumerable<Funcionario> getFuncionariosByEmpresa(int empresaId1);

        int getCantUsuarios();
        string createResetToken(int id);
    }
}
