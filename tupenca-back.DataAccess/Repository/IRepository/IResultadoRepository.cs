﻿using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IResultadoRepository : IRepository<Resultado>
    {

        void Save();
    }
}
