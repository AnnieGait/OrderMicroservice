using CustomerApi.Data.Repositories;
using CustomerApi.Domain.Entities;
using CustomerApi.Service.Commands;
using FakeItEasy;
using FluentAssertions;

namespace CustomerApi.Service.Test.Commands
{
	public class CreateCustomerCommandHandlerTests
	{
		private readonly CreateCustomerCommandHandler _testee;
		private readonly ICustomerRepository<Customer> _repository;

		public CreateCustomerCommandHandlerTests()
		{
			_repository = A.Fake<ICustomerRepository<Customer>>();
			_testee = new CreateCustomerCommandHandler(_repository);
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
