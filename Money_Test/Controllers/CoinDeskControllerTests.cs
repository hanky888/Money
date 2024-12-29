using Microsoft.AspNetCore.Mvc;
using Money.Controllers;
using Money.Dtos;
using Money.Utility;
using Money.Utility.Interafce;
using Money_Test.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Test.Controllers
{
    [TestFixture]
    public class CoinDeskControllerTests
    {
        private CoinDeskController? _controller;
        private Mock<ICoinDeskUtil> _mockCoinDeskUtil;

        [SetUp]
        public void SetUp()
        {
            // 初始化 Mock 依賴項  
            _mockCoinDeskUtil = new Mock<ICoinDeskUtil>();

            // 初始化 Controller  
            _controller = new CoinDeskController(_mockCoinDeskUtil.Object);
        }

        [TearDown]
        public void TearDown()
        {
            // 釋放 Controller 資源  
            _controller?.Dispose();
        }

        [Test]
        public async Task GetCoinDeskData_ShouldReturnData_WhenDataIsAvailable()
        {
            // Arrange  
            var mockData = MockCoinDeskData.GetMockCoinDeskInfo();
            _mockCoinDeskUtil.Setup(x => x.GetCoinDeskDataAsync()).ReturnsAsync(mockData);

            // Act  
            var result = await _controller!.GetCoinDeskData() as OkObjectResult;

            // Assert  
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(mockData));
        }

        [Test]
        public async Task GetCoinDeskData_ShouldHandleErrorGracefully_WhenExceptionOccurs()
        {
            // Arrange  
            _mockCoinDeskUtil.Setup(x => x.GetCoinDeskDataAsync()).ThrowsAsync(new Exception("API Error"));

            // Act  
            var result = await _controller.GetCoinDeskData() ;

            // Assert  
            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var objectResult = result as ObjectResult;
            Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
            Assert.That(objectResult.Value, Is.EqualTo("An error occurred while fetching data from CoinDesk."));
        }
    }
}
