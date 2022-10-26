using System;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IPencaEmpresaRepository : IRepository<PencaEmpresa>
    {

        IEnumerable<PencaEmpresa> GetPencaEmpresas();

        void Save();

    }
}

