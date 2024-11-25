using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreWebApi.Controllers;
using StoreWebApi.Interfaces;
using StoreWebApi.Models;

namespace StoreWebApi.Tests.Controllers
{
    public class CustomersControllerTests
    {
        private Mock<ICustomerService> _serviceMock;
        private CustomersController _controller;

        [SetUp]
        public void SetUp()
        {
            _serviceMock = new Mock<ICustomerService>();
            _controller = new CustomersController(_serviceMock.Object);
        }

        [Test]
        public async Task GetCustomer_ShouldReturnOk_WhenCustomerExists()
        {
            // Arrange
            var customerId = 1;
            var customer = new Customer { Id = customerId, Name = "Ion Popescu" };
            _serviceMock.Setup(service => service.GetById(customerId))
                                .ReturnsAsync(customer);

            // Act
            var result = await _controller.GetCustomer(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
            Assert.That(okResult?.Value, Is.EqualTo(customer));
        }

        [Test]
        public async Task GetCustomer_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = 999;
            _serviceMock.Setup(service => service.GetById(customerId))
                                .ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.GetCustomer(customerId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
            var notFoundResult = result.Result as NotFoundResult;
            Assert.That(notFoundResult?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task GetCustomer_ShouldReturnBadRequest_WhenIdIsInvalid()
        {
            // Act
            var result = (await _controller.GetCustomer(0));

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
            var badReqResult = result.Result as BadRequestResult;
            Assert.NotNull(badReqResult);
            Assert.That(badReqResult.StatusCode, Is.EqualTo(400));
            
        }

    }
}
