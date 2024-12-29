using Money.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Test.Mock
{
    public class MockCoinDeskData
    {
        public static CoinDeskInfo GetMockCoinDeskInfo()
        {
            return new CoinDeskInfo
            {
                Time = new Time
                {
                    Updated = "Dec 28, 2024 03:02:17 UTC",
                    UpdatedISO = "2024-12-28T03:02:17+00:00",
                    UpdatedUK = "Dec 28, 2024 at 03:02 GMT"
                },
                Disclaimer = "This data was produced from the CoinDesk Bitcoin Price Index (USD).",
                ChartName = "Bitcoin",
                Bpi = new Dictionary<string, BpiInfo>
            {
                {
                    "USD", new BpiInfo
                    {
                        Code = "USD",
                        Symbol = "$",
                        Rate = "94,291.652",
                        Description = "United States Dollar",
                        RateFloat = 94291.6516m
                    }
                },
                {
                    "GBP", new BpiInfo
                    {
                        Code = "GBP",
                        Symbol = "£",
                        Rate = "75,195.706",
                        Description = "British Pound Sterling",
                        RateFloat = 75195.7063m
                    }
                }
            }
            };
        }
    }
}
