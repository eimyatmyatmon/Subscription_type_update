namespace SubscriptionService.Models
{
    public enum SubscriptionStatus
    {
        Pending, Active, Inactive
    }

    public class Subscription : BaseModel
    {
        public Guid? UserId { get; set; }
        public Guid PaymentTransactionId { get; set; } = default!;
        public SubscriptionStatus? Status { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public Guid? GatewaySubscriptionId { get; set; }
        public string? Currency { get; set; }
        public float? Amount { get; set; }
        public Guid SubscriptionPlanId { get; set; } = default!;
        public Transaction? Transaction { get; set; }
        public SubscriptionPlan? SubscriptionPlan { get; set; }

    }
}