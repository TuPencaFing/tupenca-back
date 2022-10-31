using System;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IPencaEmpresaRepository : IRepository<PencaEmpresa>
    {

        IEnumerable<PencaEmpresa> GetPencaEmpresas();

        IEnumerable<PencaEmpresa> GetPencaEmpresasByEmpresa(int id);        

        void Save();

    }
}

