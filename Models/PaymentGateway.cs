namespace SubscriptionService.Models
{
    public class PaymentGateway : BaseModel
    {

        public PaymentGateway()
        {
            SubscriptionPlans = new HashSet<SubscriptionPlan>();
        }
        public string? Platform { get; set; }
        public bool? Active { get; set; } = false;

        public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; }
    }

}