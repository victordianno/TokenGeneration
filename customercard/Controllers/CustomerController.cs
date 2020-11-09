using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using customercard.Data;
using customercard.Models;

namespace customercard.Controllers
{
    [ApiController]
    [Route("v1/customers")]
    public class CustomerController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Customer>>> Get([FromServices] DataContext context)
        {
            var customers = await context.Customers.ToListAsync();
            return customers;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Customer>> Post(
    [FromServices] DataContext context,
    [FromBody] Customer model)
        {
            if (ModelState.IsValid)
            {
                context.Customers.Add(model);
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