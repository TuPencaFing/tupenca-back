using System;
using Microsoft.EntityFrameworkCore;
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

        public void AddCampeonato(Campeonato campeonato)
        {
            if (campeonato != null)
            {
                _campeonatoRepository.Add(campeonato);
                _campeonatoRepository.Save();
            }
        }

        public void UpdateCampeonato(int id, Campeonato campeonato)
        {
            if (campeonato != null)
            {
                var entity = findCampeonato(id);

                entity.Name = campeonato.Name;
                entity.StartDate = campeonato.StartDate;
                entity.FinishDate = campeonato.FinishDate;

                _campeonatoRepository.Update(entity);
                _campeonatoRepository.Save();
            }
        }

        public void RemoveCampeonato(Campeonato campeonato)
        {
            if (campeonato != null)
            {
                _campeonatoRepository.Remove(campeonato);
                _campeonatoRepository.Save();
            }
        }


        public bool CampeonatoExists(int id)
        {
            return findCampeonato(id) == null;
        }

    }
}

