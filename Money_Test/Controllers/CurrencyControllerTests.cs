using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Money.Controllers;
using Money.Dtos;
using Money.Models;
using Money.Utility;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Money.Utility.Interafce;

namespace Money_Test.Controllers
{
    [TestFixture]
    public class CurrencyControllerTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private CurrencyController _controller;
        private Mock<ICoinDeskUtil> _mockCoinDeskUtil;
        private Mock<DbSet<Currency>> _mockCurrencyDbSet;

        [SetUp]
        public void SetUp()
        {
              
            var mockCurrencyData = new List<Currency>
            {
                new Currency { Id = 1, Code = "TWD", Name = "新台幣" },
                new Currency { Id = 2, Code = "USD", Name = "美元" }
            }.AsQueryable();

            _mockCurrencyDbSet = new Mock<DbSet<Currency>>();
            _mockCurrencyDbSet.As<IQueryable<Currency>>().Setup(m => m.Provider).Returns(mockCurrencyData.Provider);
            _mockCurrencyDbSet.As<IQueryable<Currency>>().Setup(m => m.Expression).Returns(mockCurrencyData.Expression);
            _mockCurrencyDbSet.As<IQueryable<Currency>>().Setup(m => m.ElementType).Returns(mockCurrencyData.ElementType);
            _mockCurrencyDbSet.As<IQueryable<Currency>>().Setup(m => m.GetEnumerator()).Returns(mockCurrencyData.GetEnumerator());

            _mockCurrencyDbSet.As<IAsyncEnumerable<Currency>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new MockAsyncEnumerator<Currency>(mockCurrencyData.GetEnumerator()));
         
            _mockContext = new Mock<ApplicationDbContext>();
            _mockContext.Setup(c => c.Currency).Returns(_mockCurrencyDbSet.Object);

            
            _mockCoinDeskUtil = new Mock<ICoinDeskUtil>();

            // 初始化 Controller  
            _controller = new CurrencyController(_mockContext.Object, _mockCoinDeskUtil.Object);
        }

        [TearDown]
        public void TearDown()
        {            
            _controller?.Dispose();
            _controller = null;  
        }

        [Test]
        public async Task GetCurrencies_ShouldReturnAllCurrencies()
        {
            // Act  
            var actionResult = await _controller.GetCurrencies();
            var result = actionResult.Result as OkObjectResult;

            // Assert  
            Assert.That(result, Is.Not.Null);
            var currencies = result!.Value as List<Currency>;
            Assert.That(currencies, Is.Not.Null);
            Assert.That(currencies!.Count, Is.EqualTo(2));
            Assert.That(currencies[0].Code, Is.EqualTo("TWD"));
            Assert.That(currencies[1].Code, Is.EqualTo("USD"));
        }

        [Test]
        public async Task GetCurrency_ShouldReturnCurrency_WhenCurrencyExists()
        {
            // Act  
            var actionResult = await _controller.GetCurrency(1);
            var result = actionResult.Result as OkObjectResult;

            // Assert  
            Assert.That(result, Is.Not.Null);
            var currency = result!.Value as Currency;
            Assert.That(currency, Is.Not.Null);
            Assert.That(currency!.Code, Is.EqualTo("TWD"));
        }

        [Test]
        public async Task GetCurrency_ShouldReturnNotFound_WhenCurrencyDoesNotExist()
        {
            // Act  
            var result = await _controller.GetCurrency(999);

            // Assert  
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task PostCurrency_ShouldAddCurrency()
        {
            // Arrange  
            var newCurrency = new Currency { Id = 3, Code = "EUR", Name = "歐元" };

            // Act  
            var actionResult = await _controller.PostCurrency(newCurrency);
            var result = actionResult.Result as CreatedAtActionResult;

            // Assert  
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(201));

            // Verify that the new currency was added  
            _mockCurrencyDbSet.Verify(m => m.Add(It.IsAny<Currency>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task GetCurrencyRates_ShouldReturnCurrencyRates_WhenDataIsAvailable()
        {
            // Arrange  
            var mockApiResponse = new CoinDeskInfo
            {
                Time = new Time { Updated = "Dec 28, 2024 00:03:00 UTC" },
                Bpi = new Dictionary<string, BpiInfo>
                {
                    { "USD", new BpiInfo { Code = "USD", RateFloat = 29384.50m } }
                }
            };

            // 模擬 CoinDesk API 的響應  
            _mockCoinDeskUtil.Setup(x => x.GetCoinDeskDataAsync()).ReturnsAsync(mockApiResponse);

            // Act  
            var actionResult = await _controller.GetCurrencyRates();
            var result = actionResult.Result as OkObjectResult;

            // Assert  
            Assert.That(result, Is.Not.Null);
            var currencyRates = result!.Value as List<CurrencyRate>;
            Assert.That(currencyRates, Is.Not.Null);
            Assert.That(currencyRates!.Count, Is.EqualTo(1));
            Assert.That(currencyRates[0].Code, Is.EqualTo("USD"));
            Assert.That(currencyRates[0].RateFloat, Is.EqualTo(29384.50m));
        }
    }
}

