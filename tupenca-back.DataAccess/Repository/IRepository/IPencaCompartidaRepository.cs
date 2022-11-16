using System;
using System.Linq.Expressions;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IPencaCompartidaRepository : IRepository<PencaCompartida>
    {

        IEnumerable<PencaCompartida> GetPencaCompartidas();
       
        PencaCompartida GetFirst(Expression<Func<PencaCompartida, bool>> filter);

        int GetCantActivas();
     
        void Save();

    }
}

