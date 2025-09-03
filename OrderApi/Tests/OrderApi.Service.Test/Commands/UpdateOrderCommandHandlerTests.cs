using FakeItEasy;
using Microsoft.Extensions.Logging;
using OrderApi.Data.Repository;
using OrderApi.Domain;
using OrderApi.Service.Commands;

namespace OrderApi.Service.Test.Commands
{
	public class UpdateOrderCommandHandlerTests
	{
		private readonly UpdateOrderCommandHandler _testee;
		private readonly IRepository<Order> _repository;
		private readonly ILogger<UpdateOrderCommandHandler> _logger;

		public UpdateOrderCommandHandlerTests()
		{
			_repository = A.Fake<IRepository<Order>>();
			_logger = A.Fake<ILogger<UpdateOrderCommandHandler>>();

			_testee = new UpdateOrderCommandHandler(_repository, _logger);
		}

		[Fact]
		public async void Handle_ShouldCallRepositoryAddAsync()
		{
			await _testee.Handle(new UpdateOrderCommand(), default);

			A.CallTo(() => _repository.UpdateRangeAsync(A<List<Order>>._)).MustHaveHappenedOnceExactly();
		}
	}
}
