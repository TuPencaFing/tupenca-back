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
      

        public PremioService(ILogger<PremioService> logger,
                             IPremioRepository premioRepository)
        {
            _logger = logger;
            _premioRepository = premioRepository;
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


        
    }
}

