using System;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;
using tupenca_back.Services;
using tupenca_back.Services.Exceptions;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;

namespace tupenca_back.Controllers
{

    [ApiController]
    [Route("api/pencas-compartidas")]
    [Authorize]
    public class PencaCompartidaController : ControllerBase
    {

        private readonly ILogger<PencaCompartidaController> _logger;
        public readonly IMapper _mapper;
        private readonly PencaService _pencaService;
        private readonly PrediccionService _prediccionService;
        private readonly CampeonatoService _campeonatoService;
        private readonly EquipoService _equipoService;
        private readonly ResultadoService _resultadoService;
        private readonly PuntajeService _puntajeService;
        private readonly EventoService _eventoService;

        public PencaCompartidaController(ILogger<PencaCompartidaController> logger,
                               IMapper mapper,
                               PencaService pencaService,
                               PrediccionService prediccionService,
                               CampeonatoService campeonatoService,
                               EquipoService equipoService,
                               ResultadoService resultadoService,
                               PuntajeService puntajeService,
                               EventoService eventoService)
        {
            _logger = logger;
            _mapper = mapper;
            _pencaService = pencaService;
            _prediccionService = prediccionService;
            _campeonatoService = campeonatoService;
            _equipoService = equipoService;
            _resultadoService = resultadoService;
            _puntajeService = puntajeService;
            _eventoService = eventoService;
        }

        //GET: api/pencas-compartidas
        [HttpGet]
        public ActionResult<IEnumerable<PencaCompartidaDto>> GetPencasCompartida()
        {
            try
            {
                var pencas = _pencaService.GetPencaCompartidas();

                var pencasDto = _mapper.Map<List<PencaCompartidaDto>>(pencas);

                return Ok(pencasDto);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //GET: api/pencas-compartidas/1
        [HttpGet("{id}")]
        public ActionResult<PencaCompartidaDto> GetPencaCompartida(int id)
        {
            try
            {
                var penca = _pencaService.findPencaCompartidaById(id);

                if (penca == null)
                {
                    return NotFound();
                }
                else
                {
                    var pencaDto = _mapper.Map<PencaCompartidaDto>(penca);

                    return Ok(pencaDto);
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //POST: api/pencas-compartidas
        [HttpPost]
        public IActionResult PostPencaCompartida(PencaCompartidaDto pencaCompartidaDto)
        {
            if (pencaCompartidaDto == null)
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "La Penca no debe ser nulo");

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var pencaCompartida = _mapper.Map<PencaCompartida>(pencaCompartidaDto);

                _pencaService.AddPencaCompartida(pencaCompartida);

                return CreatedAtAction("GetPencaCompartida", new { id = pencaCompartida.Id }, _mapper.Map<PencaCompartidaDto>(pencaCompartida));
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // PUT: api/pencas-compartidas/1
        [HttpPut("{id}")]
        public IActionResult PutPencaCompartida(int id, PencaCompartidaDto pencaCompartidaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_pencaService.PencaCompartidaExists(id))
                return NotFound();

            try
            {
                var pencaCompartida = _mapper.Map<PencaCompartida>(pencaCompartidaDto);

                _pencaService.UpdatePencaCompartida(id, pencaCompartida);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // DELETE: api/pencas-compartidas/1
        [HttpDelete("{id}")]
        public IActionResult DeletePencaCompartida(int id)
        {
            try
            {
                _pencaService.RemovePencaCompartida(id);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //Pencas de cada usuario

        [HttpGet("me")]
        public ActionResult<IEnumerable<PencaCompartida>> GetPencasCompartidasByUsuario([FromQuery] bool joined)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (joined == true)
            {
                try
                {
                    var pencas = _pencaService.GetPencasCompartidasByUsuario(Convert.ToInt32(userId));
                    return Ok(pencas);
                }
                catch (Exception e)
                {
                    throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
                }
            }
            else
            {
                try
                {
                    var pencas = _pencaService.GetPencasCompartidasNoJoinedByUsuario(Convert.ToInt32(userId));
                    return Ok(pencas);
                }
                catch (Exception e)
                {
                    throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
                }
            }

        }

        [HttpPost("{id}/add")]
        public async Task<IActionResult> AddUsuarioToPencaCompartidaAsync(int id, [FromBody] PagosTarjetaDto pago)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var penca = _pencaService.findPencaCompartidaById(id);
                if (penca == null) return BadRequest();
                if (penca.CostEntry != pago.transaction_amount) return BadRequest();
                //pago
                MercadoPagoConfig.AccessToken = "TEST-5999360588657313-111213-86de986d139e8b73944ea408b6e3478f-1228301365";


                var paymentRequest = new PaymentCreateRequest
                {
                    TransactionAmount = pago.transaction_amount,
                    Token = pago.token,
                    Installments = pago.installments,
                    PaymentMethodId = pago.payment_method_id,
                    Payer = new PaymentPayerRequest
                    {
                        Email = pago.payer.email,
                        Identification = new IdentificationRequest
                        {
                            Type = pago.payer.identification.type,
                            Number = pago.payer.identification.number,
                        }
                    }
                };

                Console.WriteLine("obtengo datos pago:");
                Console.WriteLine(paymentRequest.Payer.Email);

                var client = new PaymentClient();
                Payment payment = await client.CreateAsync(paymentRequest);
                Console.WriteLine(payment.Status);
                if (payment.Status == "approved")
                {
                    _pencaService.AddUsuarioToPencaCompartida(Convert.ToInt32(userId), id);
                    //habilitar usuario
                    _pencaService.HabilitarUsuario(id, Convert.ToInt32(userId));
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }             
                              

            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}/usuarios")]
        public ActionResult<IEnumerable<UsuarioScore>> GetUsuariosPencaCompartida(int id)
        {
            try
            {
                var usuarios = _prediccionService.GetUsuariosByPenca(id);
                return Ok(usuarios);

            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        // PATCH: api/deportes/1/image        
        [HttpPatch("{id}/image")]
        public ActionResult UploadImage(int id, [FromForm] ImagenDto imagenDto)
        {
            try
            {
                _pencaService.SaveImagen(id, imagenDto.file, true);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET: api/pencas-compartidas/1/evento/1/estadisticas     
        [HttpGet("{id}/evento/{idEvento}/estadisticas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EstadisticaEventoDto> GetEstadisticas(int id, int idEvento)
        {
            var penca = _pencaService.findPencaCompartidaById(id);
            if (penca == null) return NotFound();
            EstadisticaEventoDto eventodto = new EstadisticaEventoDto();
            var porcentajeLocal = _prediccionService.GetPorcentajeLocal(id, idEvento);
            eventodto.PorcentajeLocal= porcentajeLocal;
            var evento = _eventoService.getEventoById(idEvento);
            if (evento.IsEmpateValid)
            {
                if (porcentajeLocal == null)
                {
                    eventodto.PorcentajeVisitante = null;
                    eventodto.PorcentajeEmpate = null;
                }
                else
                {
                    var porcentajeEmpate = _prediccionService.GetPorcentajeEmpate(id, idEvento);
                    eventodto.PorcentajeEmpate = porcentajeEmpate;
                    eventodto.PorcentajeVisitante = (100 - (porcentajeLocal + porcentajeEmpate));
                }
            }
            else
            {
                eventodto.PorcentajeEmpate = null;
                if (porcentajeLocal == null)
                {
                    eventodto.PorcentajeVisitante = null;
                }
                else
                {
                    eventodto.PorcentajeVisitante = (100 - porcentajeLocal);
                }
            }                      
            return Ok(eventodto);
        }

        [HttpGet("{id}/info")]
        public ActionResult<PencaInfoDto> GetInfoPenca(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var penca = _pencaService.findPencaCompartidaById(id);
                if (penca == null)
                {
                    return NotFound();
                }
                PencaInfoDto pencainfo = new PencaInfoDto();                
                pencainfo.Id = penca.Id;
                pencainfo.PencaTitle = penca.Title;
                pencainfo.PencaDescription = penca.Description;
                pencainfo.Image = penca.Image;
                pencainfo.Pozo = penca.Pozo;
                var campeonato = _campeonatoService.findCampeonatoById(penca.Campeonato.Id);
                pencainfo.CampeonatoName = campeonato.Name;
                pencainfo.StartDate = campeonato.StartDate;
                pencainfo.FinishDate = campeonato.FinishDate;
                DeporteDto deportedto = new DeporteDto();
                deportedto.Id = campeonato.Deporte.Id;
                deportedto.Nombre = campeonato.Deporte.Nombre;
                deportedto.Image = campeonato.Deporte.Image;
                pencainfo.Deporte = deportedto;                
                List<EventoPrediccionDto> eventos = new List<EventoPrediccionDto>();
                var puntaje = _puntajeService.getPuntajeById(penca.PuntajeId);
                int? puntajeTotal = 0;
                foreach (var evento in penca.Campeonato.Eventos)
                {
                    var prediccion = _prediccionService.GetPrediccionByUsuarioEvento(Convert.ToInt32(userId), evento.Id, penca.Id);
                    var resultado = _resultadoService.getResultadoByEventoId(evento.Id);
                    var equipolocal = _equipoService.getEquipoById(evento.EquipoLocalId);
                    var equipovisitante = _equipoService.getEquipoById(evento.EquipoVisitanteId);
                    
                    EventoPrediccionDto eventoinfo = new EventoPrediccionDto
                    {
                        Id = evento.Id,
                        EquipoLocal = equipolocal,
                        EquipoVisitante = equipovisitante,
                        FechaInicial = evento.FechaInicial,
                        Resultado = resultado,
                        Prediccion = prediccion
                    };
                    eventos.Add(eventoinfo);
                    if (prediccion != null)
                    {
                        if (prediccion.Score != null)
                        {
                            puntajeTotal += prediccion.Score;
                        }
                    }
                }
                pencainfo.Eventos = eventos;
                pencainfo.PuntajeTotal = puntajeTotal;                

                return Ok(pencainfo);
                
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }       
    }
}

