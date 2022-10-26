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
        private readonly EventoService _eventoService;


        public CampeonatoService(ICampeonatoRepository campeonatoRepository,
            DeporteService deporteService, EventoService eventoService )
        {
            _campeonatoRepository = campeonatoRepository;
            _deporteService = deporteService;
            _eventoService = eventoService;
        }

        public IEnumerable<Campeonato> getCampeonatos() => _campeonatoRepository.GetCampeonatos();

        public Campeonato? findCampeonatoById(int? id) => _campeonatoRepository.GetFirstOrDefault(c => c.Id == id);

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
                var campeonatoToUpdate = findCampeonatoById(id);

                if (campeonatoToUpdate == null)
                {
                    throw new NotFoundException("El Campeonato no existe");
                }

                campeonatoToUpdate.Name = campeonato.Name;
                campeonatoToUpdate.StartDate = campeonato.StartDate;
                campeonatoToUpdate.FinishDate = campeonato.FinishDate;

                _campeonatoRepository.Update(campeonatoToUpdate);
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
            return findCampeonatoById(id) == null;
        }

        public bool CampeonatoNameExists(string name)
        {
            return findCampeonatoByName(name) != null;
        }

        public Campeonato addEvento(int id, Evento evento)
        {
            var campeonato = findCampeonatoById(id);

            if (campeonato == null)
            {
                throw new NotFoundException("Campeonato no encontrado");
            }

            var eventoToAdd = _eventoService.getEventoById(evento.Id);


            if (eventoToAdd == null)
            {
                throw new NotFoundException("Evento no encontrado");
            }

            campeonato.Eventos.Add(eventoToAdd);

            _campeonatoRepository.Update(campeonato);
            _campeonatoRepository.Save();

            return campeonato;

        }

    }
}

