using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using AutoMapper;
using System.Net;
using tupenca_back.Services.Exceptions;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Route("api/empresas")]
    public class EmpresaController : ControllerBase
    {
        private readonly ILogger<EmpresaController> _logger;
        public readonly IMapper _mapper;
        private readonly EmpresaService _empresaService;
        private readonly PlanService _planService;

        public EmpresaController(ILogger<EmpresaController> logger,
                                 IMapper mapper,
                                 EmpresaService empresaService,
                                 PlanService planService)
        {
            _logger = logger;
            _mapper = mapper;
            _empresaService = empresaService;
            _planService = planService;
        }

        //GET: api/empresas        
        [HttpGet]
        public ActionResult<IEnumerable<EmpresaDto>> GetEmpresas()
        {
            var empresas = _empresaService.getEmpresas();
            var empresasDto = _mapper.Map<List<EmpresaDto>>(empresas);
            return Ok(empresasDto);
        }

        //GET: api/empresas/nuevas        
        [HttpGet("nuevas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<int> GetEmpresasNuevas()
        {
            int empresas = _empresaService.GetCantEmpresasNuevas();
            return Ok(empresas);
        }

        // GET: api/empresas/1        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Empresa> GetEmpresaById(int id)
        {
            var empresa = _empresaService.getEmpresaById(id);

            if (empresa == null)
            {
                return NotFound();
            } 
            else
            {
                return Ok(empresa);
            }
        }


        // POST: api/empresas       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Empresa>> CreateEmpresaAsync(EmpresaPaymentDto empresaDto)
        {
            Empresa empresa = new Empresa();
            empresa.RUT = empresaDto.RUT;
            empresa.Razonsocial = empresaDto.Razonsocial;
            empresa.FechaCreacion = DateTime.Now;


            var plan = _planService.FindPlanById(empresaDto.PlanId);
            if (plan == null)
                throw new NotFoundException("El Plan no existe");
            empresa.Plan = plan;            

            var emp = _empresaService.getEmpresaByRUT(empresa.RUT);
            if (emp != null)
            {
                return BadRequest("La empresa ingresada ya existe");
            }

            
            
            if (plan.Cost != empresaDto.Pago.transaction_amount) return BadRequest();

            //pago
            MercadoPagoConfig.AccessToken = "TEST-5999360588657313-111213-86de986d139e8b73944ea408b6e3478f-1228301365";

            var paymentRequest = new PaymentCreateRequest
            {
                TransactionAmount = empresaDto.Pago.transaction_amount,
                Token = empresaDto.Pago.token,
                Installments = empresaDto.Pago.installments,
                PaymentMethodId = empresaDto.Pago.payment_method_id,
                Payer = new PaymentPayerRequest
                {
                    Email = empresaDto.Pago.payer.email,
                    Identification = new IdentificationRequest
                    {
                        Type = empresaDto.Pago.payer.identification.type,
                        Number = empresaDto.Pago.payer.identification.number,
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
                _empresaService.CreateEmpresa(empresa);
                //return CreatedAtAction("GetEmpresaById", new { id = empresa.Id }, _mapper.Map<EmpresaDto>(empresa));
                return CreatedAtAction("GetEmpresaById", new { id = empresa.Id }, empresa);
            }
            else
            {
                return BadRequest();
            }
            
            /*
            _empresaService.CreateEmpresa(empresa);

            //return CreatedAtAction("GetEmpresaById", new { id = empresa.Id }, _mapper.Map<EmpresaDto>(empresa));
            return CreatedAtAction("GetEmpresaById", new { id = empresa.Id }, empresa);
            */
        }


        // DELETE: api/empresas/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEmpresas(int id)
        {
            var empresa = _empresaService.getEmpresaById(id);

            if (empresa == null)
            {
                return NotFound();
            }
            _empresaService.RemoveEmpresa(empresa);
            return NoContent();
        }


        // PATCH: api/empresas/1/image        
        [HttpPatch("{id}/image")]
        public ActionResult UploadImage(int id, [FromForm] ImagenDto imagenDto)
        {
            try
            {
                _empresaService.SaveImagen(id, imagenDto.file);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // Patch: api/campeonatos/1/eventos
        [HttpPatch("{id}/plan")]
        public ActionResult<EmpresaDto> ChangePlan(int id, PlanDto plan)
        {
            try
            {
                var empresa = _empresaService.ChangePlan(id, plan.Id);

                return Ok(_mapper.Map<EmpresaDto>(empresa));
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
    }
}

