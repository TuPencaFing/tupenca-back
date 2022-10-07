using System;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class CampeonatoService
    {
        private readonly ICampeonatoRepository _campeonatoRepository;

        public CampeonatoService(ICampeonatoRepository campeonatoRepository)
        {
            _campeonatoRepository = campeonatoRepository;
        }

        public IEnumerable<Campeonato> getCampeonatos() => _campeonatoRepository.GetAll();

        public Campeonato? findCampeonato(int? id) => _campeonatoRepository.GetFirstOrDefault(c => c.Id == id);

        public void addCampeonato(Campeonato campeonato)
        {
            if (campeonato != null)
            {
                _campeonatoRepository.Add(campeonato);
                _campeonatoRepository.Save();
            }
        }

        public void editCampeonato(Campeonato campeonato)
        {
            if (campeonato != null)
            {
                _campeonatoRepository.Update(campeonato);
                _campeonatoRepository.Save();
            }
        }

        public void deleteUser(Campeonato campeonato)
        {
            if (campeonato != null)
            {
                _campeonatoRepository.Remove(campeonato);
                _campeonatoRepository.Save();
            }
        }
    }
}

