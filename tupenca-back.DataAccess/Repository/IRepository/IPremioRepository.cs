﻿using System;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IPremioRepository : IRepository<Premio>
    {
        void Save();
    }
}
