using Microsoft.AspNetCore.Http;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class ForoService
    {
        private readonly IForoRepository _foroRepository;

        public ForoService(IForoRepository foroRepository)
        {
            _foroRepository = foroRepository;
        }

        public IEnumerable<ForoUsers> getMessagesByPenca(int pencaId) => _foroRepository.getMessagesByPenca(pencaId);

        public void CreateMessage(Foro foro)
        {
            if (foro != null)
            {
                _foroRepository.Add(foro);
                _foroRepository.Save();
            }
        }

        public void UpdateMessage(Foro foro)
        {
            if (foro != null)
            {
                _foroRepository.Update(foro);
                _foroRepository.Save();
            }
        }

        public void RemoveMessage(Foro foro)
        {
            if (foro != null)
            {
                _foroRepository.Remove(foro);
                _foroRepository.Save();
            }
        }

        public Foro getMessageById(int id)
        {
            return _foroRepository.GetFirstOrDefault(f => f.Id == id);
        }

    }
}

