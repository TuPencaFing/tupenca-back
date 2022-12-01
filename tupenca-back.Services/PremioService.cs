using System;
using Microsoft.Extensions.Logging;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Services
{
    public class PremioService
    {
        private readonly ILogger<PremioService> _logger;
        private readonly IPremioRepository _premioRepository;
        private readonly IUsuarioPremioRepository _usuarioPremioRepository;
        private readonly CampeonatoService _campeonatoService;
        private readonly PencaService _pencaService;
        private readonly PuntajeUsuarioPencaService _puntajeUsuarioPencaService;

        public PremioService(ILogger<PremioService> logger,
                             IPremioRepository premioRepository,
                             IUsuarioPremioRepository usuarioPremioRepository,
                             CampeonatoService campeonatoService,
                             PencaService pencaService,
                             PuntajeUsuarioPencaService puntajeUsuarioPencaService)
        {
            _logger = logger;
            _premioRepository = premioRepository;
            _usuarioPremioRepository = usuarioPremioRepository;
            _campeonatoService = campeonatoService;
            _puntajeUsuarioPencaService = puntajeUsuarioPencaService;
        }

        public IEnumerable<Premio> GetPremios() => _premioRepository.GetAll();

        public Premio? FindPremioById(int? id) => _premioRepository.GetFirstOrDefault(c => c.Id == id);

        public void AddPremio(Premio premio)
        {
            _premioRepository.Add(premio);
            _premioRepository.Save();
        }

        public void UpdatePremio(int id, Premio premio)
        {
            var premioToUpdate = FindPremioById(id);

            if (premioToUpdate == null)
                throw new NotFoundException("La Premio no existe");

            premioToUpdate.Position = premio.Position;
            premioToUpdate.Percentage = premio.Percentage;

            _premioRepository.Update(premioToUpdate);
            _premioRepository.Save();
        }

        public void RemovePremio(int id)
        {
            var premio = FindPremioById(id);

            if (premio == null)
                throw new NotFoundException("La Premio no existe");

            _premioRepository.Remove(premio);
            _premioRepository.Save();
        }

        public bool PremioExists(int id)
        {
            return FindPremioById(id) == null;
        }


        public void AsignarPremio()
        {
            var campeonatosFinalizados = _campeonatoService.GetCampeonatosFinalized();

            foreach (var campeonatoFinalizado in campeonatosFinalizados)
            {
                // Obtengo las pencas del campeonato finalziado
                foreach (var penca in campeonatoFinalizado.Pencas)
                {
                    bool esPencacompartida = penca is PencaCompartida;

                    // Chequeo que las pencas tengan premios por entregar
                    if (!penca.PremiosEntregados)
                    {

                        var usuariosPuntaje = _puntajeUsuarioPencaService.GetAllByPenca(penca.Id).ToList();

                        foreach (var premio in penca.Premios)
                        {
                            if (usuariosPuntaje.Count >= premio.Position)
                            {
                                var idUsuario = usuariosPuntaje[premio.Position - 1].UsuarioId;

                                UsuarioPremio usuarioPremio = new UsuarioPremio();
                                usuarioPremio.IdPenca = penca.Id;
                                usuarioPremio.Reclamado = false;
                                usuarioPremio.PendientePago = true;
                                usuarioPremio.IdUsuario = idUsuario;


                                if (esPencacompartida)
                                {
                                    var pencaCompartida = (PencaCompartida) penca;

                                    var pozoSinComision = pencaCompartida.Pozo * (1 - pencaCompartida.Commission / 100);

                                    usuarioPremio.Premio = pozoSinComision * (premio.Percentage / 100);
                                }
                                else
                                {
                                    // todo ver que hacer 
                                    //var pencaEmpresa = (PencaEmpresa) penca;
                                    //var premioUsuario = 
                                }

                                _usuarioPremioRepository.Add(usuarioPremio);
                                _usuarioPremioRepository.Save();
                            }
                        }

                        // Guardo premios entregados en penca
                        penca.PremiosEntregados = true;

                        if (esPencacompartida)
                        {
                            var pencaCompartida = (PencaCompartida) penca;
                            _pencaService.UpdatePencaCompartida(pencaCompartida.Id, pencaCompartida);
                        }
                        else
                        {
                            var pencaEmpresa = (PencaEmpresa) penca;
                            _pencaService.UpdatePencaEmpresa(pencaEmpresa.Id, pencaEmpresa);
                        }

                    }
                }

                // Guardo premios entregados en campeonato
                campeonatoFinalizado.PremiosEntregados = true;
                _campeonatoService.UpdateCampeonato(campeonatoFinalizado.Id, campeonatoFinalizado);
            }
        }
    }
}

