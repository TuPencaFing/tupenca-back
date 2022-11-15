using System;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Services
{
	public class PuntajeUsuarioPencaService
	{
        private readonly IPuntajeUsuarioPencaRepository _puntajeUsuarioPencaRepository;
        private readonly EventoService _eventoService;
        private readonly PrediccionService _prediccionService;


        public PuntajeUsuarioPencaService(IPuntajeUsuarioPencaRepository puntajeUsuarioPencaRepository,
                                          EventoService eventoService,
                                          PrediccionService prediccionService)
        {
            _puntajeUsuarioPencaRepository = puntajeUsuarioPencaRepository;
            _eventoService = eventoService;
            _prediccionService = prediccionService;
        }


        public PuntajeUsuarioPenca? FindById(int id) => _puntajeUsuarioPencaRepository.GetFirstOrDefault(e => e.Id == id);

        public PuntajeUsuarioPenca? GetByPencaAndUsuario(int pencaId, int usuarioId) => _puntajeUsuarioPencaRepository
            .GetFirstOrDefault(pup => pup.PencaId == pencaId && pup.UsuarioId == usuarioId);

        public IEnumerable<PuntajeUsuarioPenca> GetAllByPenca(int pencaId) => _puntajeUsuarioPencaRepository.GetAllByPenca(pencaId);

        public IEnumerable<PuntajeUsuarioPenca> GetAllByUsuario(int usuarioId) => _puntajeUsuarioPencaRepository.GetAllByUsuario(usuarioId);


        public void Create(PuntajeUsuarioPenca puntajeUsuarioPenca)
        {
            if (puntajeUsuarioPenca != null)
            {
                _puntajeUsuarioPencaRepository.Add(puntajeUsuarioPenca);
                _puntajeUsuarioPencaRepository.Save();
            }
        }


        public void Update(int id, PuntajeUsuarioPenca puntajeUsuarioPenca)
        {
            if (puntajeUsuarioPenca != null)
            {
                var puntajeToUpdate = FindById(id);

                if (puntajeToUpdate == null)
                {
                    throw new NotFoundException("El Puntaje de Usuario no existe");
                }

                puntajeToUpdate.Score = puntajeUsuarioPenca.Score;

                _puntajeUsuarioPencaRepository.Update(puntajeToUpdate);
                _puntajeUsuarioPencaRepository.Save();
            }
        }

        public PuntajeUsuarioPenca AgregarScore(int pencaId, int usuarioId, int score)
        {
            var puntaje = GetByPencaAndUsuario(pencaId, usuarioId);

            if (puntaje == null)
            {
                var puntajeNuevo = new PuntajeUsuarioPenca();
                puntajeNuevo.PencaId = pencaId;
                puntajeNuevo.UsuarioId = usuarioId;
                puntajeNuevo.Score += score;

                Create(puntajeNuevo);

                return puntajeNuevo;
            }
            else
            {
                puntaje.Score += score;
                _puntajeUsuarioPencaRepository.Update(puntaje);
                _puntajeUsuarioPencaRepository.Save();
                return puntaje;
            }

        }

        //todo terminar calculo de score como estaba antes
        public void CalcularPuntajes(int eventoId, Resultado resultado)
        {
            var evento = _eventoService.getEventoById(eventoId);

            if (evento != null)
            {
                var predicciones = _prediccionService.getPrediccionesByEvento(evento.Id);

                foreach (var prediccion in predicciones)
                {
                    
                }
            }

            
        }
    }
}

