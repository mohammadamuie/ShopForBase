namespace Project.Application.Models
{
    public class SendFiterNotification
    {
        public string app_ids { get; set; }
        public DeviceFilters filters { get; set; }
        public NotificationData data { get; set; }
    }
}
