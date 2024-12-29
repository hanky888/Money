using Money.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Money.Utility;
using Money.Dtos;
using Money.Utility.Interafce;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Money.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ICoinDeskUtil _coinDeskUtil;

        public CurrencyController(ApplicationDbContext context, ICoinDeskUtil coinDeskUtil)
        {
            _context = context;
            _coinDeskUtil = coinDeskUtil;
        }
  
        // GET: api/Currencies  
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            return await _context.Currency.ToListAsync();
        }

        // GET: api/Currency/1
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<Currency>> GetCurrency(int id)
        {
            var currency = await _context.Currency.FindAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // POST: api/Currency  
        [HttpPost]
        public virtual async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
            _context.Currency.Add(currency);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurrency), new { id = currency.Id }, currency);
        }

        // PUT: api/Currency/1  
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutCurrency(int id, Currency currency)
        {
            if (id != currency.Id)
            {
                return BadRequest();
            }

            _context.Entry(currency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Currency/1 
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteCurrency(int id)
        {
            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }

        [HttpGet("GetCurrencyRates")]        
        public virtual async Task<ActionResult<IEnumerable<CurrencyRate>>> GetCurrencyRates()
        {
            var coinDeskInfo = await _coinDeskUtil.GetCoinDeskDataAsync();

            var timeUpdated = coinDeskInfo.Time.Updated;
            var updatedDateTime = DateTime.ParseExact(timeUpdated, "MMM dd, yyyy HH:mm:ss 'UTC'", CultureInfo.InvariantCulture)
                                             .ToString("yyyy/MM/dd HH:mm:ss");

            var currencyRates = new List<CurrencyRate>();

            foreach (var currency in coinDeskInfo.Bpi.Values)
            {
                var code = currency.Code;
                var rateFloat = currency.RateFloat;

                var currencyEntity = await _context.Currency
                    .FirstOrDefaultAsync(c => c.Code == code);

                if (currencyEntity != null)
                {
                    currencyRates.Add(new CurrencyRate
                    {
                        Code = code,
                        Name = currencyEntity.Name,
                        RateFloat = rateFloat,
                        UpdatedTime = updatedDateTime
                    });
                }
            }


            return Ok(currencyRates);
        }
    }
}
