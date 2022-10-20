﻿using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        int GetCantEmpresasNuevas();

        void Save();

    }
}
