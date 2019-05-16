using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRPushNotification.Server.Models
{
    public class PushNotificationModel
    {
        public string SelectedMenu { get; set; }
        public string Message { get; set; }
        public string CallerConnectionId { get; set; }
        public string ReciversConnectionIds { get; set; }
    }
}
