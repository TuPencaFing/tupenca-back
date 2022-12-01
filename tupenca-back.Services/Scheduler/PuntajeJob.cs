using System;
using Quartz;

namespace tupenca_back.Services.Scheduler
{
	public class PuntajeJob : IJob
    {

        private readonly PremioService _premioService;

        public PuntajeJob(PremioService premioService)
        {
            _premioService = premioService;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            _premioService.AsignarPremio();
            await Task.CompletedTask;
        }
    }
}

