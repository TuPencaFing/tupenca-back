using System.Linq.Expressions;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IEquipoRepository : IRepository<Equipo>
    {
        void Save();

    }
}
