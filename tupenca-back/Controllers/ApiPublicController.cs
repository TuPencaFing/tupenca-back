using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Controllers.Dto;
using tupenca_back.Services;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/public")]
    public class ApiPublicController : ControllerBase
    {

        private readonly ResultadoService _resultadoService;
        private readonly EventoService _eventoService;

        public ApiPublicController(ResultadoService resultadoService,
                                   EventoService eventoService)
        {
            _resultadoService = resultadoService;
            _eventoService = eventoService;
        }


        [HttpGet("resultados")]
        public ActionResult<IEnumerable<ApiPublicaResultadoDto>> GetResultados()
        {
            var resultadosDto = new List<ApiPublicaResultadoDto>();

            var resultados = _resultadoService.getResultados();

            foreach (var resultado in resultados)
            {
                var resultadoDto = new ApiPublicaResultadoDto();
                var eventoDto = new ApiPublicaEventoDto();
                var equipoLocalDto = new ApiPublicaEquipoDto();
                var equipoVisitanteDto = new ApiPublicaEquipoDto();


                resultadoDto.Resultado = resultado.resultado;
                resultadoDto.PuntajeEquipoLocal = resultado.PuntajeEquipoLocal;
                resultadoDto.PuntajeEquipoVisitante = resultado.PuntajeEquipoVisitante;
                resultadoDto.Evento = eventoDto;

                // Evento
                var evento = _eventoService.getEventoById(resultado.EventoId);
               
                eventoDto.Image = evento.Image;
                eventoDto.EquipoLocal = equipoLocalDto;
                eventoDto.EquipoVisitante = equipoVisitanteDto;


                // Equipo local
                var equipoLocal = evento.EquipoLocal;

                equipoLocalDto.Nombre = equipoLocal.Nombre;
                equipoLocalDto.Image = equipoLocal.Image;

                // Equipo local
                var equipoVisitante = evento.EquipoVisitante;

                equipoVisitanteDto.Nombre = equipoVisitante.Nombre;
                equipoVisitanteDto.Image = equipoVisitante.Image;


                resultadosDto.Add(resultadoDto);
            }

            return resultadosDto;
        }

    }
}

