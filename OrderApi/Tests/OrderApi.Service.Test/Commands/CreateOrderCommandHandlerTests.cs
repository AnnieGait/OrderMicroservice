using FakeItEasy;
using FluentAssertions;
using OrderApi.Data.Repository;
using OrderApi.Domain;
using OrderApi.Service.Commands;

namespace OrderApi.Service.Test.Commands
{
	public class CreateOrderCommandHandlerTests
	{
		private readonly IRepository<Order> _repository;
		private readonly CreateOrderCommandHandler _testee;

		public CreateOrderCommandHandlerTests()
		{
			_repository = A.Fake<IRepository<Order>>();
			_testee = new CreateOrderCommandHandler(_repository);
		}

		[Fact]
		public async void Handle_ShouldReturnCreatedOrder()
		{
			A.CallTo(() => _repository.AddAsync(A<Order>._)).Returns(new Order { CustomerFullName = "Bruce Wayne" });

			var result = await _testee.Handle(new CreateOrderCommand(), default);

			result.Should().BeOfType<Order>();
			result.CustomerFullName.Should().Be("Bruce Wayne");
		}

		[Fact]
		public async void Handle_ShouldCallRepositoryAddAsync()
		{
			await _testee.Handle(new CreateOrderCommand(), default);

			A.CallTo(() => _repository.AddAsync(A<Order>._)).MustHaveHappenedOnceExactly();
		}
	}
}
