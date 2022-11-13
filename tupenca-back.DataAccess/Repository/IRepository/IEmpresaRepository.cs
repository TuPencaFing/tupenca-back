using System.Linq.Expressions;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        int GetCantEmpresasNuevas();

        int GetCantEmpresas();

        Empresa GetFirst(Expression<Func<Empresa, bool>> filter);

        IEnumerable<Empresa> GetEmpresas();

        void Save();

    }
}
