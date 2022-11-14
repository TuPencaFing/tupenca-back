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
        UserInviteToken getUserInviteToken(string access_token);
        void RemoveUserToken(UserInviteToken usertoken);
        void Save();
        IEnumerable<Funcionario> getFuncionariosByEmpresa(int empresaId1);

        int getCantUsuarios();
    }
}
