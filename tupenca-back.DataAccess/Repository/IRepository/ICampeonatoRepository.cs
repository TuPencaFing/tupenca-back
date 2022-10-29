using System;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface ICampeonatoRepository : IRepository<Campeonato>
    {
        IEnumerable<Campeonato> GetCampeonatos();

        Campeonato? FindCampeonatoById(int id);

        void Save();
    }
}

