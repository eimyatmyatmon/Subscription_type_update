using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SubscriptionService.Data;

namespace SubscriptionService.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]

    public class CustomController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<CustomController> _logger;

        public CustomController(DataContext dbContext, ILogger<CustomController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        [HttpGet("QualifySubscription")]
        public IActionResult QualifySubscription()
        {
            var transactions = _dbContext.Transactions;
            if (transactions != null)
            {
                return Subscribe();
            }
            return Unsubscribe();
        }
        private IActionResult Subscribe()
        {
            var subscriptions = _dbContext.Subscriptions;

            var expiredDates = subscriptions.Select(s => s.ExpiredAt);
            var subscribe = new JsonSerializerSettings
            {
                DateFormatString = "MM/dd/yyyy hh:mm:ss tt"
            };
            var json = JsonConvert.SerializeObject(new { expiredDates = expiredDates }, subscribe);
            return Content(json, "application/json");
        }
        private IActionResult Unsubscribe()
        {
            var subscriptions = _dbContext.Subscriptions;

            var expiredDates = subscriptions.Select(s => s.ExpiredAt);
            var unsubscribe = new JsonSerializerSettings
            {
                DateFormatString = "MM/dd/yyyy hh:mm:ss tt"
            };
            var json = JsonConvert.SerializeObject(new { expiredDates = expiredDates }, unsubscribe);
            return Content(json, "application/json");
        }
    }
}
