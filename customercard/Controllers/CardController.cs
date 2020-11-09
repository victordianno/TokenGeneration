using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using customercard.Data;
using customercard.Models;

namespace customercard.Controllers
{
    [ApiController]
    [Route("v1/cards")]
    public class CardController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Card>>> Get([FromServices] DataContext context)
        {
            var cards = await context.Cards.ToListAsync();
            return cards;
        }

        [HttpGet]
        [Route("{customerID:int}")]
        public async Task<ActionResult<Card>>GetById([FromServices] DataContext context, int customerID)
        {
            var cards = await context.Cards.Include(x => x.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerId == customerID);
            return cards;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Card>> Post(
    [FromServices] DataContext context,
    [FromBody] Card model)
        {
            if (ModelState.IsValid)
            {
                context.Cards.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}