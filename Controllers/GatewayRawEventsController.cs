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
    public class GatewayRawEventsController : ODataController
    {
        private readonly DataContext _db;

        private readonly ILogger<GatewayRawEventsController> _logger;

        public GatewayRawEventsController(DataContext dbContext, ILogger<GatewayRawEventsController> logger)
        {
            _logger = logger;
            _db = dbContext;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<GatewayRawEvent> Get()
        {
            return _db.GatewayRawEvents;
        }

        [EnableQuery]
        public SingleResult<GatewayRawEvent> Get([FromODataUri] Guid key)
        {
            var result = _db.GatewayRawEvents.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] GatewayRawEvent gatewayRawEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.GatewayRawEvents.Add(gatewayRawEvent);
            await _db.SaveChangesAsync();
            return Created(gatewayRawEvent);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] Guid key, Delta<GatewayRawEvent> GatewayRawEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingGatewayRawEvent = await _db.GatewayRawEvents.FindAsync(key);
            if (existingGatewayRawEvent == null)
            {
                return NotFound();
            }

            GatewayRawEvent.Patch(existingGatewayRawEvent);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GatewayRawEventExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(existingGatewayRawEvent);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var existingGatewayRawEvent = await _db.GatewayRawEvents.FindAsync(key);
            if (existingGatewayRawEvent == null)
            {
                return NotFound();
            }

            _db.GatewayRawEvents.Remove(existingGatewayRawEvent);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool GatewayRawEventExists(Guid key)
        {
            return _db.GatewayRawEvents.Any(p => p.Id == key);
        }
    }
}