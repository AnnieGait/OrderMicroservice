using MediatR;
using Microsoft.Extensions.Logging;
using OrderApi.Data.Repository;
using OrderApi.Domain;

namespace OrderApi.Service.Commands
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
	{
		private readonly IRepository<Order> _repository;
		private readonly ILogger<CreateOrderCommandHandler> _logger;

		public CreateOrderCommandHandler(
			IRepository<Order> repository,
			ILogger<CreateOrderCommandHandler> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var response = await _repository.AddAsync(request.Order);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Entity could not be saved");
				throw;
			}
		}
	}
}
