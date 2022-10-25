using System;
using Microsoft.Extensions.Logging;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

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

        public Premio? findPremioById(int? id) => _premioRepository.GetFirstOrDefault(c => c.Id == id);
    }
}

