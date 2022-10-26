using System;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Services
{
    public class PlanService
    {

        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public IEnumerable<Plan> GetPlanes() => _planRepository.GetAll();

        public Plan? FindPlanById(int? id) => _planRepository.GetFirstOrDefault(c => c.Id == id);

        public void AddPlan(Plan plan)
        {
            _planRepository.Add(plan);
            _planRepository.Save();
        }

        public void UpdatePlan(int id, Plan plan)
        {
            var planToUpdate = FindPlanById(id);

            if (planToUpdate == null)
                throw new NotFoundException("El Plan no existe");

            planToUpdate.CantUser = plan.CantUser;
            planToUpdate.PercentageCost = plan.PercentageCost;
            planToUpdate.LookAndFeel = plan.LookAndFeel;

            _planRepository.Update(planToUpdate);
            _planRepository.Save();
        }

        public void RemovePlan(int id)
        {
            var plan = FindPlanById(id);

            if (plan == null)
                throw new NotFoundException("El Plan no existe");

            _planRepository.Remove(plan);
            _planRepository.Save();
        }

        public bool PlanExists(int id)
        {
            return FindPlanById(id) == null;
        }

    }
}

