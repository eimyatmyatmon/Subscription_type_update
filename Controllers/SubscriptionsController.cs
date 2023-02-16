using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Data;
using SubscriptionService.Models;

namespace SubscriptionService.Controllers
{
    public class SubscriptionsController : ODataController
    {
        private readonly DataContext _db;

        private readonly ILogger<SubscriptionsController> _logger;

        public SubscriptionsController(DataContext dbContext, ILogger<SubscriptionsController> logger)
        {
            _logger = logger;
            _db = dbContext;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<Subscription> Get()
        {
            return _db.Subscriptions;
        }

        [EnableQuery]
        public SingleResult<Subscription> Get([FromODataUri] Guid key)
        {
            var result = _db.Subscriptions.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Subscriptions.Add(subscription);
            await _db.SaveChangesAsync();
            return Created(subscription);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] Guid key, Delta<Subscription> subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingSubscription = await _db.Subscriptions.FindAsync(key);
            if (existingSubscription == null)
            {
                return NotFound();
            }

            subscription.Patch(existingSubscription);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(existingSubscription);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var existingSubscription = await _db.Subscriptions.FindAsync(key);
            if (existingSubscription == null)
            {
                return NotFound();
            }

            _db.Subscriptions.Remove(existingSubscription);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool SubscriptionExists(Guid key)
        {
            return _db.Subscriptions.Any(p => p.Id == key);
        }
    }
}