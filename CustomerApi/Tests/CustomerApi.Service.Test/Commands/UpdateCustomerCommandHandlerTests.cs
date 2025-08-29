using CustomerApi.Data.Repositories;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Sender;
using CustomerApi.Service.Commands;
using FakeItEasy;
using FluentAssertions;

namespace CustomerApi.Service.Test.Commands
{
	public class UpdateCustomerCommandHandlerTests
	{
		private readonly UpdateCustomerCommandHandler _testee;
		private readonly ICustomerRepository<Customer> _repository;
		private readonly ICustomerUpdateSender _customerUpdateSender;
		private readonly Customer _customer;

		public UpdateCustomerCommandHandlerTests()
		{
			_repository = A.Fake<ICustomerRepository<Customer>>();
			_customerUpdateSender = A.Fake<ICustomerUpdateSender>();
			_testee = new UpdateCustomerCommandHandler(_repository, _customerUpdateSender);

			_customer = new Customer
			{
				FirstName = "Yoda"
			};
		}

		[Fact]
		public async void Handle_ShouldCallCustomerUpdaterSenderSendCustomer()
		{
			A.CallTo(() => _repository.UpdateAsync(A<Customer>._)).Returns(_customer);

			await _testee.Handle(new UpdateCustomerCommand { Customer = new Customer() }, default);

			A.CallTo(() => _customerUpdateSender.SendCustomer(_customer)).MustHaveHappenedOnceExactly();
		}

		[Fact]
		public async void Handle_ShouldReturnUpdatedCustomer()
		{
			A.CallTo(() => _repository.UpdateAsync(A<Customer>._)).Returns(_customer);

			var result = await _testee.Handle(new UpdateCustomerCommand { Customer = new Customer() }, default);

			result.Should().BeOfType<Customer>();
			result.FirstName.Should().Be(_customer.FirstName);
		}

		[Fact]
		public async void Handle_ShouldUpdateAsync()
		{
			await _testee.Handle(new UpdateCustomerCommand { Customer = new Customer() }, default);

			A.CallTo(() => _repository.UpdateAsync(A<Customer>._)).MustHaveHappenedOnceExactly();
		}
	}
}
