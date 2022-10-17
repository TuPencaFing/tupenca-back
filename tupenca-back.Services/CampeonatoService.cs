using System;
using Microsoft.EntityFrameworkCore;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Services
{
    public class CampeonatoService
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly DeporteService _deporteService;

        public CampeonatoService(ICampeonatoRepository campeonatoRepository, DeporteService deporteService)
        {
            _campeonatoRepository = campeonatoRepository;
            _deporteService = deporteService;
        }

        public IEnumerable<Campeonato> getCampeonatos() => _campeonatoRepository.GetAll();

        public Campeonato? findCampeonato(int? id) => _campeonatoRepository.GetFirstOrDefault(c => c.Id == id);

        public Campeonato? findCampeonatoByName(string name) => _campeonatoRepository.GetFirstOrDefault(c => c.Name == name);

        public void AddCampeonato(Campeonato campeonato)
        {
            var deporte = _deporteService.getDeporteById(campeonato.Deporte.Id);

            if (deporte == null)
            {
                throw new NotFoundException("El Deporte no existe");
            }

            campeonato.Deporte = deporte;

            _campeonatoRepository.Add(campeonato);
            _campeonatoRepository.Save();
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

        public bool CampeonatoNameExists(string name)
        {
            return findCampeonatoByName(name) != null;
        }

    }
}

