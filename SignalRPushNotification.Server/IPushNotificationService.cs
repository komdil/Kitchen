using SignalRPushNotification.Server.Models;
using System.Threading.Tasks;

namespace SignalRPushNotification.Server
{
    public interface IPushNotificationService
    {
        Task SendAsync(PushNotificationModel notification);
    }
}
