using System;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IPlanRepository : IRepository<Plan>
    {
        void Save();
    }
}

