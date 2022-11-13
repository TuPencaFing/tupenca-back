using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IUsuarioPencaRepository : IRepository<UsuarioPenca>
    {
        IEnumerable<UsuarioPenca> GetUsuarioPencas(int id);

        IEnumerable<PencaCompartida> GetUsuarioPencasCompartidas(int id);

        IEnumerable<PencaCompartida> GetUsuarioPencasCompartidasNoJoined(int id);

        IEnumerable<PencaEmpresa> GetUsuarioPencasEmpresa(int empresaId, int id);


        IEnumerable<Evento> GetEventosProximosPencaCompartida(int id, int pencaid);

        IEnumerable<Evento> GetEventosProximosPencas(int id);

        int GetCantUsuariosPenca(int id);

        void Save();

        public void HabilitarUsuario(int pencaId, int usuarioId);

    }
}
