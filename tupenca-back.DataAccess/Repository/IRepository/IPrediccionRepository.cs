﻿using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IPrediccionRepository : IRepository<Prediccion>
    {

        void Save();
    }
}