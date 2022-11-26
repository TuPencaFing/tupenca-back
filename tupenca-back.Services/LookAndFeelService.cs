using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class LookAndFeelService
    {
        private readonly ILookAndFeelRepository _lookandfeelRepository;

        public LookAndFeelService(ILookAndFeelRepository lookandfeelRepository)
        {
            _lookandfeelRepository = lookandfeelRepository;
        }

        public void CreateLookAndFeel(LookAndFeel estilos)
        {
            if (estilos != null)
            {
                _lookandfeelRepository.Add(estilos);
                _lookandfeelRepository.Save();
            }
        }

        public void UpdateLookAndFeel(LookAndFeel estilos)
        {
            if (estilos != null)
            {
                _lookandfeelRepository.Update(estilos);
                _lookandfeelRepository.Save();
            }
        }

        public void RemoveLookAndFeel(LookAndFeel estilos)
        {
            if (estilos != null)
            {
                _lookandfeelRepository.Remove(estilos);
                _lookandfeelRepository.Save();
            }
        }

        public LookAndFeel getLookAndFeelByEmpresaId(int idEmpresa)
        {
            return _lookandfeelRepository.GetFirstOrDefault(f => f.EmpresaId == idEmpresa);
        }

    }
}

