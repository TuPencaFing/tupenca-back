using CorePush.Google;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime;
using System.Threading.Tasks;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using tupenca_back.Model.Notification;
using static tupenca_back.Model.Notification.GoogleNotification;

namespace tupenca_back.Services
{
    public interface INotificationService
    {
        void RegisterDeviceId(int userId, string deviceId);
        Task<ResponseModel> SendNotification(NotificationModel notificationModel, int? pencaId = null);
        public void SendScore(int eventoId, Resultado resultado);

    }

    public class NotificationService : INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        private readonly IPersonaRepository _personaRepository;
        private readonly IEventoRepository _eventoRepository;


        public NotificationService(IOptions<FcmNotificationSetting> settings, IPersonaRepository personaRepository, IEventoRepository eventoRepository)
        {
            _personaRepository = personaRepository;
            _eventoRepository = eventoRepository;
            _fcmNotificationSetting = settings.Value;
        }

        public void SendScore(int eventoId, Resultado resultado)
        {
            var usersIds = _personaRepository.getUsersWithPredictionOfEvento(eventoId);
            var deviceIds = _personaRepository.getUsersNotificationTokens(usersIds);
            var evento = _eventoRepository.GetFirst(e => e.Id == eventoId);
            foreach (var id in deviceIds)
            {
                var message = new NotificationModel() {Title = "Partido Finalizado", IsAndroiodDevice = true, DeviceId = id, Body = evento.EquipoLocal.Nombre+ " " + resultado.PuntajeEquipoLocal + " - " + evento.EquipoVisitante.Nombre + " " + resultado.PuntajeEquipoVisitante };
                SendNotification(message);
            }
        }

        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel, int? pencaId = null)
        {
            pencaId = 12;
            ResponseModel response = new ResponseModel();
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {
                    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _fcmNotificationSetting.SenderId,
                        ServerKey = _fcmNotificationSetting.ServerKey
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    NotificationPayload dataPayload = new NotificationPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    if(pencaId != null)
                    {
                        DataPayload dataPayload2 = new DataPayload();
                        dataPayload2.pencaId = pencaId.ToString();
                        notification.Data = dataPayload2;
                    }
                    else
                    {
                        notification.Data = null;

                    }
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }

        public void RegisterDeviceId(int userId, string deviceId)
        {
            _personaRepository.createNotificationDeviceId(userId, deviceId);
        }
    }
}