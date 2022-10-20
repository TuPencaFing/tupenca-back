using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public IEnumerable<Empresa> getEmpresas() => _empresaRepository.GetAll();

        public Empresa? getEmpresaById(int? id) => _empresaRepository.GetFirstOrDefault(e => e.Id == id);

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



    }
}

