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
    public class PaymentGatewaysController : ODataController
    {
        private readonly DataContext _db;

        private readonly ILogger<PaymentGatewaysController> _logger;

        public PaymentGatewaysController(DataContext dbContext, ILogger<PaymentGatewaysController> logger)
        {
            _logger = logger;
            _db = dbContext;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<PaymentGateway> Get()
        {
            return _db.PaymentGateways;
        }

        [EnableQuery]
        public SingleResult<PaymentGateway> Get([FromODataUri] Guid key)
        {
            var result = _db.PaymentGateways.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] PaymentGateway paymentGateway)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.PaymentGateways.Add(paymentGateway);
            await _db.SaveChangesAsync();
            return Created(paymentGateway);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] Guid key, Delta<PaymentGateway> Todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingPaymentGateway = await _db.PaymentGateways.FindAsync(key);
            if (existingPaymentGateway == null)
            {
                return NotFound();
            }

            Todo.Patch(existingPaymentGateway);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoPaymentGateway(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(existingPaymentGateway);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var existingPaymentGateway = await _db.PaymentGateways.FindAsync(key);
            if (existingPaymentGateway == null)
            {
                return NotFound();
            }

            _db.PaymentGateways.Remove(existingPaymentGateway);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private bool TodoPaymentGateway(Guid key)
        {
            return _db.PaymentGateways.Any(p => p.Id == key);
        }
    }
}