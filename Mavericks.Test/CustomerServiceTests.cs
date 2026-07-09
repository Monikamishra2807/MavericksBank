using MavericksBank.DTOs;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using MavericksBank.Services.Implementation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace MavericksBank.Tests.Services
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private Mock<ICustomerRepository> _repository;
        private CustomerService _service;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<ICustomerRepository>();

            _service = new CustomerService(
                _repository.Object,
                NullLogger<CustomerService>.Instance);
        }

        [Test]
        public async Task When_GetAllCustomers_ReturnsCustomerList()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    UserId = 10,
                    Address = "Chennai",
                    DOB = new DateTime(2002,5,20),
                    AadharNumber = "123456789012",
                    PanNumber = "ABCDE1234F",

                    User = new User
                    {
                        UserId = 10,
                        FullName = "Monika Mishra",
                        Email = "monika@gmail.com",
                        Mobile = "9876543210"
                    }
                }
            };

            _repository.Setup(x => x.GetAllCustomersAsync())
                       .ReturnsAsync(customers);

            // Act
            var result = await _service.GetAllCustomersAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));

            var customer = result.First();

            Assert.That(customer.FullName, Is.EqualTo("Monika Mishra"));
            Assert.That(customer.Email, Is.EqualTo("monika@gmail.com"));
            Assert.That(customer.Mobile, Is.EqualTo("9876543210"));
        }

        [Test]
        public async Task When_GetCustomerById_ValidId_ReturnsCustomer()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = 1,
                UserId = 10,
                Address = "Chennai",
                DOB = new DateTime(2002, 5, 20),
                AadharNumber = "123456789012",
                PanNumber = "ABCDE1234F",

                User = new User
                {
                    UserId = 10,
                    FullName = "Monika Mishra",
                    Email = "monika@gmail.com",
                    Mobile = "9876543210"
                }
            };

            _repository.Setup(x => x.GetCustomerByIdAsync(1))
                       .ReturnsAsync(customer);

            // Act
            var result = await _service.GetCustomerByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.CustomerId, Is.EqualTo(1));
            Assert.That(result.FullName, Is.EqualTo("Monika Mishra"));
            Assert.That(result.Email, Is.EqualTo("monika@gmail.com"));
            Assert.That(result.Mobile, Is.EqualTo("9876543210"));
        }

        [Test]
        public async Task When_CreateCustomer_ValidCustomer_CallsRepository()
        {
            // Arrange
            var dto = new CustomerDto
            {
                UserId = 10,
                Address = "Coimbatore",
                DOB = new DateTime(2003, 2, 2),
                AadharNumber = "987654321098",
                PanNumber = "PQRSX5678Y"
            };

            // Act
            await _service.CreateCustomerAsync(dto);

            // Assert
            _repository.Verify(x => x.AddAsync(It.IsAny<Customer>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task When_UpdateCustomer_ValidCustomer_UpdatesCustomer()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = 1,
                UserId = 10,
                Address = "Old Address",
                DOB = new DateTime(2002, 1, 1),
                AadharNumber = "123456789012",
                PanNumber = "ABCDE1234F",

                User = new User
                {
                    UserId = 10,
                    FullName = "Monika Mishra",
                    Email = "monika@gmail.com",
                    Mobile = "9876543210"
                }
            };

            var dto = new CustomerDto
            {
                Address = "New Address",
                DOB = new DateTime(2002, 1, 1),
                AadharNumber = "123456789012",
                PanNumber = "ABCDE1234F"
            };

            _repository.Setup(x => x.GetCustomerByIdAsync(1))
                       .ReturnsAsync(customer);

            // Act
            await _service.UpdateCustomerAsync(1, dto);

            // Assert
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Customer>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void When_UpdateCustomer_InvalidId_ThrowsException()
        {
            // Arrange
            _repository.Setup(x => x.GetCustomerByIdAsync(100))
                       .ReturnsAsync((Customer?)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _service.UpdateCustomerAsync(100, new CustomerDto()));

            Assert.That(ex.Message, Is.EqualTo("Customer not found."));
        }

        [Test]
        public async Task When_DeleteCustomer_ValidId_DeletesCustomer()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = 1,

                User = new User
                {
                    UserId = 10,
                    FullName = "Monika Mishra",
                    Email = "monika@gmail.com",
                    Mobile = "9876543210"
                }
            };

            _repository.Setup(x => x.GetCustomerByIdAsync(1))
                       .ReturnsAsync(customer);

            // Act
            await _service.DeleteCustomerAsync(1);

            // Assert
            _repository.Verify(x => x.DeleteAsync(It.IsAny<Customer>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}