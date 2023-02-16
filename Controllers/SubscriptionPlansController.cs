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
    public class SubscriptionPlansController : ODataController
    {
        private readonly DataContext _db;

        private readonly ILogger<SubscriptionPlansController> _logger;

        public SubscriptionPlansController(DataContext dbContext, ILogger<SubscriptionPlansController> logger)
        {
            _logger = logger;
            _db = dbContext;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<SubscriptionPlan> Get()
        {
            return _db.SubscriptionPlans;
        }

        [EnableQuery]
        public SingleResult<SubscriptionPlan> Get([FromODataUri] Guid key)
        {
            var result = _db.SubscriptionPlans.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] SubscriptionPlan note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.SubscriptionPlans.Add(note);
            await _db.SaveChangesAsync();
            return Created(note);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] Guid key, Delta<SubscriptionPlan> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingSubscriptionPlan = await _db.SubscriptionPlans.FindAsync(key);
            if (existingSubscriptionPlan == null)
            {
                return NotFound();
            }

            note.Patch(existingSubscriptionPlan);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionPlanExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(existingSubscriptionPlan);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var existingSubscriptionPlan = await _db.SubscriptionPlans.FindAsync(key);
            if (existingSubscriptionPlan == null)
            {
                return NotFound();
            }

            _db.SubscriptionPlans.Remove(existingSubscriptionPlan);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool SubscriptionPlanExists(Guid key)
        {
            return _db.SubscriptionPlans.Any(p => p.Id == key);
        }
    }
}