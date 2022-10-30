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

        IEnumerable<PencaEmpresa> GetUsuarioPencasEmpresa(int id);

        IEnumerable<Evento> GetEventosProximosPencasCompartidas(int id);

        IEnumerable<Evento> GetEventosProximosPencas(int id);

        void Save();
    }
}
