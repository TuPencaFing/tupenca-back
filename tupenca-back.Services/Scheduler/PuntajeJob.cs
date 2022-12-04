using System;
using Quartz;

namespace tupenca_back.Services.Scheduler
{
	public class PuntajeJob : IJob
    {

        private readonly EntregaPremioService _entregaPremioService;

        public PuntajeJob(EntregaPremioService entregaPremioService)
        {
            _entregaPremioService = entregaPremioService;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            _entregaPremioService.AsignarPremio();
            await Task.CompletedTask;
        }
    }
}

