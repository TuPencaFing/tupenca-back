﻿using System;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IPencaCompartidaRepository : IRepository<PencaCompartida>
    {

        IEnumerable<PencaCompartida> GetPencaCompartidas();
     
        void Save();

    }
}
