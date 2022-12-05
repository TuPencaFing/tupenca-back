using System;
using Microsoft.AspNetCore.Http;
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
        private readonly ImagesService _imagesService;


        public CampeonatoService(ICampeonatoRepository campeonatoRepository,
                                 DeporteService deporteService,
                                 EventoService eventoService,
                                 ImagesService imagesService)
        {
            _campeonatoRepository = campeonatoRepository;
            _deporteService = deporteService;
            _eventoService = eventoService;
            _imagesService = imagesService;
        }

        public IEnumerable<Campeonato> getCampeonatos() => _campeonatoRepository.GetCampeonatos();

        public Campeonato? findCampeonatoById(int id) => _campeonatoRepository.FindCampeonatoById(id);

        public Campeonato? findCampeonatoByName(string name) => _campeonatoRepository.GetFirstOrDefault(c => c.Name == name);

        public IEnumerable<Campeonato>? SearchCampeonato(string searchString) => _campeonatoRepository.SearchCampeonato(searchString);

        public void AddCampeonato(Campeonato campeonato)
        {
            var deporte = _deporteService.getDeporteById(campeonato.Deporte.Id);

            if (deporte == null)
            {
                throw new NotFoundException("El Deporte no existe");
            }

            campeonato.Deporte = deporte;

            var eventos = new List<Evento>();

            campeonato.Eventos.ForEach(e => eventos.Add(e));

            campeonato.Eventos.Clear();

            foreach (Evento evento in eventos)
            {
                var eventoToAdd = _eventoService.getEventoById(evento.Id);

                if (eventoToAdd == null)
                {
                    throw new NotFoundException("Evento no encontrado");
                }

                campeonato.Eventos.Add(eventoToAdd);
            }

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

                if (campeonato.Deporte.Id != null)
                {
                    var deporte = _deporteService.getDeporteById(campeonato.Deporte.Id);

                    if (deporte == null)
                        throw new NotFoundException("El Deporte no encontrado");

                    campeonatoToUpdate.Deporte = deporte;
                }

                campeonatoToUpdate.Name = campeonato.Name;
                campeonatoToUpdate.Image = campeonato.Image;
                campeonatoToUpdate.StartDate = campeonato.StartDate;
                campeonatoToUpdate.FinishDate = campeonato.FinishDate;
                campeonatoToUpdate.PremiosEntregados = campeonato.PremiosEntregados;

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


        public void SaveImagen(int id, IFormFile file)
        {
            var campeonato = findCampeonatoById(id);

            string image = _imagesService.uploadImage(file.FileName, file.OpenReadStream());

            campeonato.Image = image;

            UpdateCampeonato(id, campeonato);
        }


        public List<Campeonato> GetCampeonatosFinalized()
        {
            return _campeonatoRepository.GetCampeonatosFinalized().ToList();
        }


    }
}

