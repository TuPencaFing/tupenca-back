using System;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class PlanService
    {

        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public Plan? findPlanById(int? id) => _planRepository.GetFirstOrDefault(c => c.Id == id);

    }
}

