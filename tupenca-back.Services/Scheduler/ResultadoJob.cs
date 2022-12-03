using System;
using Quartz;

namespace tupenca_back.Services.Scheduler
{
	public class ResultadoJob : IJob
	{

        private readonly ResultadoService _resultadoService;

        public ResultadoJob(ResultadoService resultadoService)
        {
            _resultadoService = resultadoService;
        }


        public async Task Execute(IJobExecutionContext context)
        {

            await Task.CompletedTask;
        }
    }
}

