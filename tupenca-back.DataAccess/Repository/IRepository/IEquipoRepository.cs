using System.Linq.Expressions;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IEquipoRepository : IRepository<Equipo>
    {
        public IEnumerable<Equipo>? SearchEquipo(string searchString);

        void Save();

    }
}
