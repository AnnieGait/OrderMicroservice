using CustomerApi.Data.Repositories;
using CustomerApi.Domain.Entities;
using CustomerApi.Service.Commands;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace CustomerApi.Service.Test.Commands
{
	public class CreateCustomerCommandHandlerTests
	{
		private readonly CreateCustomerCommandHandler _testee;
		private readonly ICustomerRepository<Customer> _repository;
		private readonly ILogger<CreateCustomerCommandHandler> _logger;

		public CreateCustomerCommandHandlerTests()
		{
			_repository = A.Fake<ICustomerRepository<Customer>>();
			_logger = A.Fake<ILogger<CreateCustomerCommandHandler>>();
			_testee = new CreateCustomerCommandHandler(_repository, _logger);
		}

		[Fact]
		public async void Handle_ShouldCallAddAsync()
		{
			await _testee.Handle(new CreateCustomerCommand { Customer = new Customer() }, default);

			A.CallTo(() => _repository.AddAsync(A<Customer>._)).MustHaveHappenedOnceExactly();
		}

		[Fact]
		public async void Handle_ShouldReturnCreatedCustomer()
		{
			A.CallTo(() => _repository.AddAsync(A<Customer>._)).Returns(new Customer
			{
				FirstName = "Yoda"
			});

			var result = await _testee.Handle(new CreateCustomerCommand { Customer = new Customer() }, default);

			result.Should().BeOfType<Customer>();
			result.FirstName.Should().Be("Yoda");
		}
	}
}
