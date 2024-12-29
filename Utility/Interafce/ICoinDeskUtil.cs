using Money.Dtos;

namespace Money.Utility.Interafce
{
    public interface ICoinDeskUtil
    {
        Task<CoinDeskInfo> GetCoinDeskDataAsync();
    }
}
