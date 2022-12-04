using Newtonsoft.Json.Linq;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.Utilities.EmailService;

namespace tupenca_back.Services.Scheduler
{
    public class NotificationEventoProximoJob : IJob
    {

        private readonly INotificationService _notificationService;
        private readonly UsuarioService _usuarioService;
        private readonly IEmailSender _emailSender;


        public NotificationEventoProximoJob(IEmailSender emailSender, INotificationService notificationService, UsuarioService usuarioService)
        {
            _notificationService = notificationService;
            _usuarioService = usuarioService;
            _emailSender = emailSender;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            var users = _usuarioService.getUsuarios();
            foreach (var user in users)
            {
                
                string subject = "Resumen de posiciones";
                //string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
                string htmlSend = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html\r\n  xmlns=\"http://www.w3.org/1999/xhtml\"\r\n  xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n  xmlns:v=\"urn:schemas-microsoft-com:vml\"\r\n  lang=\"en\"\r\n>\r\n  <head>\r\n    <link\r\n      rel=\"stylesheet\"\r\n      type=\"text/css\"\r\n      hs-webfonts=\"true\"\r\n      href=\"https://fonts.googleapis.com/css?family=Lato|Lato:i,b,bi\"\r\n    />\r\n    <title>Email template</title>\r\n    <meta property=\"og:title\" content=\"Email template\" />\r\n\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\r\n\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n\r\n    <style type=\"text/css\">\r\n      a {{\r\n        text-decoration: underline;\r\n        color: inherit;\r\n        font-weight: bold;\r\n        color: #253342;\r\n      }}\r\n\r\n      h1 {{\r\n        font-size: 56px;\r\n      }}\r\n\r\n      h2 {{\r\n        font-size: 28px;\r\n        font-weight: 900;\r\n      }}\r\n\r\n      p {{\r\n        font-weight: 100;\r\n      }}\r\n\r\n      td {{\r\n        vertical-align: top;\r\n      }}\r\n\r\n      #email {{\r\n        margin: auto;\r\n        width: 600px;\r\n        background-color: white;\r\n      }}\r\n\r\n      button {{\r\n        font: inherit;\r\n        background-color: #ff7a59;\r\n        border: none;\r\n        padding: 10px;\r\n        text-transform: uppercase;\r\n        letter-spacing: 2px;\r\n        font-weight: 900;\r\n        color: white;\r\n        border-radius: 5px;\r\n        box-shadow: 3px 3px #d94c53;\r\n      }}\r\n\r\n      .subtle-link {{\r\n        font-size: 9px;\r\n        text-transform: uppercase;\r\n        letter-spacing: 1px;\r\n        color: #cbd6e2;\r\n      }}\r\n    </style>\r\n  </head>\r\n\r\n  <body\r\n    bgcolor=\"#F5F8FA\"\r\n    style=\"\r\n      width: 100%;\r\n      margin: auto 0;\r\n      padding: 0;\r\n      font-family: Lato, sans-serif;\r\n      font-size: 18px;\r\n      color: #33475b;\r\n      word-break: break-word;\r\n    \"\r\n  >\r\n    <! View in Browser Link -->\r\n\r\n    <div id=\"email\">\r\n      <table align=\"right\" role=\"presentation\">\r\n        <tr>\r\n          <td>\r\n            <a class=\"subtle-link\" href=\"#\">View in Browser</a>\r\n          </td>\r\n        </tr>\r\n\r\n        <tr></tr>\r\n      </table>\r\n\r\n      <! Banner -->\r\n      <table role=\"presentation\" width=\"100%\">\r\n        <tr>\r\n          <td bgcolor=\"#00A4BD\" align=\"center\" style=\"color: white\">\r\n            <img\r\n              alt=\"Flower\"\r\n              src=\"https://tupenca.blob.core.windows.net/images/logo1.png\"\r\n              width=\"200px\"\r\n              align=\"middle\"\r\n            />\r\n\r\n            <!-- <h1>Tu Penca</h1> -->\r\n          </td>\r\n        </tr>\r\n      </table>\r\n\r\n      <! First Row -->\r\n\r\n      <table\r\n        role=\"presentation\"\r\n        border=\"0\"\r\n        cellpadding=\"0\"\r\n        cellspacing=\"10px\"\r\n        style=\"padding: 30px 30px 30px 60px\"\r\n      >\r\n        <tr>\r\n          <td>\r\n            <h2>Hola {user.UserName},</h2>\r\n                <p>Tu resumen semanal de pencas esta disponible</p>\r\n            <p>Para acceder a tu resumen semanal, apreta el boton de abajo</p>\r\n            <a href=\"https://localhost:3000/mispencas\">\r\n              <button>Ver Pencas</button>\r\n            </a>\r\n          </td>\r\n        </tr>\r\n      </table>\r\n\r\n      <! Second Row with Two Columns-->\r\n\r\n      <!-- <table\r\n        role=\"presentation\"\r\n        border=\"0\"\r\n        cellpadding=\"0\"\r\n        cellspacing=\"10px\"\r\n        width=\"100%\"\r\n        style=\"padding: 30px 30px 30px 60px\"\r\n      >\r\n        <tr>\r\n          <td>\r\n            <img\r\n              alt=\"Blog\"\r\n              src=\"https://www.hubspot.com/hubfs/assets/hubspot.com/style-guide/brand-guidelines/guidelines_sample-illustration-3.svg\"\r\n              width=\"200px\"\r\n              align=\"middle\"\r\n            />\r\n\r\n            <h2>Vivamus ac elit eget</h2>\r\n            <p>\r\n              Vivamus ac elit eget dolor placerat tristique et vulputate nibh.\r\n              Sed in elementum nisl, quis mollis enim. Etiam gravida dui vel est\r\n              euismod, at aliquam ipsum euismod.\r\n            </p>\r\n          </td>\r\n\r\n          <td>\r\n            <img\r\n              alt=\"Shopping\"\r\n              src=\"https://tupenca.blob.core.windows.net/images/logo1.png\"\r\n              width=\"200px\"\r\n              align=\"middle\"\r\n            />\r\n            <h2>Suspendisse tincidunt iaculis</h2>\r\n            <p>\r\n              Suspendisse tincidunt iaculis fringilla. Orci varius natoque\r\n              penatibus et magnis dis parturient montes, nascetur ridiculus mus.\r\n              Cras laoreet elit purus, quis pulvinar ipsum pulvinar et.\r\n            </p>\r\n          </td>\r\n        </tr>\r\n\r\n        <tr>\r\n          <td><button>Button 2</button></td>\r\n          <td><button>Button 3</button></td>\r\n        </tr>\r\n      </table> -->\r\n\r\n      <! Banner Row -->\r\n      <!-- <table\r\n        role=\"presentation\"\r\n        bgcolor=\"#EAF0F6\"\r\n        width=\"100%\"\r\n        style=\"margin-top: 50px\"\r\n      >\r\n        <tr>\r\n          <td align=\"center\" style=\"padding: 30px 30px\">\r\n            <h2>Nullam porta arcu</h2>\r\n            <p>\r\n              Nam vel lobortis lorem. Nunc facilisis mauris at elit pulvinar,\r\n              malesuada condimentum erat vestibulum. Pellentesque eros tellus,\r\n              finibus eget erat at, tempus rutrum justo.\r\n            </p>\r\n            <a href=\"#\"> Ask us a question</a>\r\n          </td>\r\n        </tr>\r\n      </table> -->\r\n\r\n      <! Unsubscribe Footer -->\r\n\r\n      <!-- <table role=\"presentation\" bgcolor=\"#F5F8FA\" width=\"100%\">\r\n        <tr>\r\n          <td align=\"left\" style=\"padding: 30px 30px\">\r\n            <p style=\"color: #99acc2\">Made with &hearts; at HubSpot HQ</p>\r\n            <a class=\"subtle-link\" href=\"#\"> Unsubscribe </a>\r\n          </td>\r\n        </tr>\r\n      </table> -->\r\n    </div>\r\n  </body>\r\n</html>\r\n";
                //Get and set the AppSettings using configuration manager.
                Message message = new Message(new string[] { user.Email }, subject, htmlSend);
               // _emailSender.SendEmail(message);
            }
            //_notificationService.SendNotification();
            Console.WriteLine("hi");
            await Task.CompletedTask;
        }
    }
}
