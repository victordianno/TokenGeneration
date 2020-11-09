using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using customercard.Data;
using customercard.Models;
using System.Linq;
using System;
using System.IO;

namespace customercard.Controllers
{
    [ApiController]
    [Route("v1/tokens")]
    public class TokenController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Token>>> Get([FromServices] DataContext context)
        {
            var tokens = await context.Tokens.ToListAsync();
            return tokens;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Token>>> GetById([FromServices] DataContext context, int id)
        {
            var tokens = await context.Tokens
                .Include(x => x.Customer)
                .Include(y => y.Card)
                .AsNoTracking()
                .Where(x => x.TokenId == id)
                .ToListAsync();


            var resposta = tokens.FirstOrDefault();
            var cvv = resposta.CVV;
            string card = resposta.Card.CardNumber.ToString();
            var cardLast4Number = card.Substring(card.Length - 5, 4);

            string valueRotation = cardLast4Number;
            for (int i = 0; i < cvv; i++)
            {
                var firstNumber = valueRotation.Substring(0,1);
                var secondNumber = valueRotation.Substring(1,1);
                var thirdNumber = valueRotation.Substring(2,1);
                var fourthNumber = valueRotation.Substring(3,1);

                //valueRotation = fourthNumber + firstNumber + secondNumber + thirdNumber;
                
            }   
            
            DateTime dateToken = resposta.DateToken;
            DateTime now = DateTime.Now;
            long dif = (now - dateToken).Minutes;

            if (dif >= 30)
            {
                return BadRequest("Token invalid");
            }
            Console.Out.WriteLine(dif);
            Console.Out.WriteLine(long.Parse(valueRotation).ToString());
            Console.Out.WriteLine(long.Parse(resposta.TokenValue.ToString()));

            if (long.Parse(valueRotation) == resposta.TokenValue)
            {
                 return tokens;
            }
            else
            {
                return BadRequest("Token invalid");
            }
            
            //return "valueRotation: " + valueRotation + "cardLast4Number: " + cardLast4Number.ToString();
           
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Token>> Post(
    [FromServices] DataContext context,
    [FromBody] Token model)
        {
            if (ModelState.IsValid)
            {
                context.Tokens.Add(model);
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