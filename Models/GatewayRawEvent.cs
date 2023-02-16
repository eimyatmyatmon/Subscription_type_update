using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace SubscriptionService.Models
{
    public class GatewayRawEvent : BaseModel
    {

        public Guid? OrderId { get; set; }
        public string? EventType { get; set; }
        /* private string? eventPayload;
         public JObject? EventPayload
         {
             get
             {
                 return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(eventPayload) ? "{}" : eventPayload);
             }
             set
             {
                 eventPayload = value.ToString();
             }
         }*/
        public JObject? EventPayload { get; set; } = null;
        public Transaction? Transaction { get; set; }
    }
}