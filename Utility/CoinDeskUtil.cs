using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Money.Utility.ObjException;
using Money.Dtos;
using Money.Utility.Interafce;

namespace Money.Utility
{
    public class CoinDeskUtil : ICoinDeskUtil
    {
        private readonly HttpClient _httpClient;             
        
        public CoinDeskUtil(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CoinDeskInfo> GetCoinDeskDataAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://api.coindesk.com/v1/bpi/currentprice.json");                                                 
                var result = JsonSerializer.Deserialize<CoinDeskInfo>(response);                                 

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }    
    }
}
