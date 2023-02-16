namespace SubscriptionService.Models
{
    public class SubscriptionPlan : BaseModel
    {
        public SubscriptionPlan()
        {
            Subscriptions = new HashSet<Subscription>();
        }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? FeaturedImage { get; set; }
        public int? Duration { get; set; }
        public Guid Gateways { get; set; } = default!;
        public string? CostDisplay { get; set; }
        public bool? Active { get; set; } = false;
        public PaymentGateway? PaymentGateway { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}