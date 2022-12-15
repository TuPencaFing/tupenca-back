using Newtonsoft.Json.Linq;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.Model;
using tupenca_back.Utilities.EmailService;

namespace tupenca_back.Services.Scheduler
{
    public class NotificationResumenPencasJob : IJob
    {

        private readonly INotificationService _notificationService;
        private readonly EventoService _eventoService;
        private readonly IEmailSender _emailSender;


        public NotificationResumenPencasJob(IEmailSender emailSender, INotificationService notificationService, EventoService eventoService)
        {
            _notificationService = notificationService;
            _eventoService = eventoService;
            _emailSender = emailSender;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            var users = _eventoService.getUsuariosWithoutPredictionForEvent();
            if (users != null)
            {
                foreach (var user in users)
                {

                    var message = new Message(new string[] { user.Email }, "Eventos pendientes sin prediccion", "");
                    var templateMessage = $"                    <h2>Hola,</h2>\r\n                    <p>Tienes eventos proximos sin prediccion</p>\r\n                                        <p>Para acceder a la los eventos, haz click en el siguiente boton</p>\r\n                    <a href=\"https://tupenca-user-front.azurewebsites.net/\">\r\n                        <button>Acceder</button>\r\n                    </a>";

                    _emailSender.SendEmailWithTemplate(message, templateMessage);
                    // _emailSender.SendEmail(message);
                }
                //_notificationService.SendNotification();
            }

            await Task.CompletedTask;
        }
    }
}
