using Microsoft.AspNetCore.Http;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ImagesService _imagesService;
        private readonly PlanService _planService;

        public EmpresaService(IEmpresaRepository empresaRepository,
                              ImagesService imagesService,
                              PlanService planService)
        {
            _empresaRepository = empresaRepository;
            _imagesService = imagesService;
            _planService = planService;
        }

        public IEnumerable<Empresa> getEmpresas() => _empresaRepository.GetEmpresas();

        public Empresa? getEmpresaById(int? id) => _empresaRepository.GetFirst(e => e.Id == id);

        public Empresa? getEmpresaByRUT(string? rut) => _empresaRepository.GetFirst(e => e.RUT == rut);

        public void CreateEmpresa(Empresa empresa)
        {
            if (empresa != null)
            {
                _empresaRepository.Add(empresa);
                _empresaRepository.Save();
            }
        }

        public void UpdateEmpresa(Empresa empresa)
        {
            if (empresa != null)
            {
                _empresaRepository.Update(empresa);
                _empresaRepository.Save();
            }
        }

        public void RemoveEmpresa(Empresa empresa)
        {
            if (empresa != null)
            {
                _empresaRepository.Remove(empresa);
                _empresaRepository.Save();
            }
        }

        public int GetCantEmpresasNuevas()
        {
            return _empresaRepository.GetCantEmpresasNuevas();        
        }

        public void SaveImagen(int id, IFormFile file)
        {
            var empresa = getEmpresaById(id);

            string image = _imagesService.uploadImage(file.FileName, file.OpenReadStream());

            empresa.Image = image;

            UpdateEmpresa(empresa);
        }

        public int CantEmpresas()
        {
            return _empresaRepository.GetCantEmpresas();
        }


        public Empresa ChangePlan(int id, int planId)
        {

            var empresa = getEmpresaById(id);

            if (empresa == null)
                throw new NotFoundException("Empresa no encontrada");

            var plan = _planService.FindPlanById(planId);


            if (plan == null)
                throw new NotFoundException("Plan no encontrado");

            empresa.PlanId = plan.Id;
            empresa.Plan = plan;

            UpdateEmpresa(empresa);

            return empresa;
        }


        public int GetGananciasPorPlan()
        {
            int ganancias = 0;

            var empresas = getEmpresas();

            foreach (var empresa in empresas)
            {
                ganancias += empresa.Plan.Cost;
            }

            return ganancias;
        }

    }
}

