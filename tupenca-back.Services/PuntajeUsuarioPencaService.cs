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
        private readonly PencaService _pencaService;
        private readonly PuntajeService _puntajeService;


        public PuntajeUsuarioPencaService(IPuntajeUsuarioPencaRepository puntajeUsuarioPencaRepository,
                                          EventoService eventoService,
                                          PrediccionService prediccionService,
                                          PencaService pencaService,
                                          PuntajeService puntajeService)
        {
            _puntajeUsuarioPencaRepository = puntajeUsuarioPencaRepository;
            _eventoService = eventoService;
            _prediccionService = prediccionService;
            _pencaService = pencaService;
            _puntajeService = puntajeService;
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


        public void CalcularPuntajes(Resultado resultado)
        {
            var evento = _eventoService.getEventoById(resultado.EventoId);

            if (evento != null)
            {
                var predicciones = _prediccionService.getPrediccionesByEvento(evento.Id);

                foreach (var prediccion in predicciones)
                {
                    var penca = _pencaService.GetPencaById(prediccion.PencaId);

                    var puntaje = _puntajeService.getPuntajeById(penca.PuntajeId);

                    int scoreGenerado = 0;

                    if (prediccion.prediccion == resultado.resultado)
                    {
                        // Acerto Resultado
                        if (evento.IsPuntajeEquipoValid)
                        {
                            if (resultado.PuntajeEquipoLocal == prediccion.PuntajeEquipoLocal
                                && resultado.PuntajeEquipoVisitante == prediccion.PuntajeEquipoVisitante)
                            {
                                scoreGenerado = puntaje.ResultadoExacto;
                            }
                            else
                            {
                                scoreGenerado = puntaje.Resultado;
                            }
                        }
                        else
                        {
                            scoreGenerado = puntaje.Resultado;
                        }
                    }

                    // Guardo el score en la prediccion, para mantener trazabilidad de puntos por evento
                    prediccion.Score = scoreGenerado;
                    _prediccionService.UpdatePrediccion(prediccion);

                    // Actualizo score general por penca
                    AgregarScore(prediccion.PencaId, prediccion.UsuarioId, scoreGenerado);

                }
            }

            
        }
    }
}

