using Microsoft.AspNetCore.Mvc;
using Money.Utility;
using Money.Utility.Interafce;

namespace Money.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinDeskController : Controller
    {
        private readonly ICoinDeskUtil _coinDeskUtil;

        public CoinDeskController(ICoinDeskUtil coinDeskUtil)
        {
            _coinDeskUtil = coinDeskUtil;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoinDeskData()
        {
            try
            {
                var data = await _coinDeskUtil.GetCoinDeskDataAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {                  
                Console.WriteLine($"Error: {ex.Message}");                
                return StatusCode(500, "An error occurred while fetching data from CoinDesk.");
            }
        }
    }
}
