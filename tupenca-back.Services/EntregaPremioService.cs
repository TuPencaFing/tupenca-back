using System;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
	public class EntregaPremioService
	{
        private readonly IUsuarioPremioRepository _usuarioPremioRepository;
        private readonly PencaService _pencaService;
        private readonly CampeonatoService _campeonatoService;
        private readonly PuntajeUsuarioPencaService _puntajeUsuarioPencaService;

        public EntregaPremioService(IUsuarioPremioRepository usuarioPremioRepository,
                                    PencaService pencaService,
                                    CampeonatoService campeonatoService,
                                    PuntajeUsuarioPencaService puntajeUsuarioPencaService)
		{
            _usuarioPremioRepository = usuarioPremioRepository;
            _pencaService = pencaService;
            _campeonatoService = campeonatoService;
            _puntajeUsuarioPencaService = puntajeUsuarioPencaService;
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
                                    var pencaCompartida = (PencaCompartida)penca;

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
                            var pencaCompartida = (PencaCompartida)penca;
                            _pencaService.UpdatePencaCompartida(pencaCompartida.Id, pencaCompartida);
                        }
                        else
                        {
                            var pencaEmpresa = (PencaEmpresa)penca;
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

