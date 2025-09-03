using MediatR;
using Microsoft.Extensions.Logging;
using OrderApi.Data.Repository;
using OrderApi.Domain;

namespace OrderApi.Service.Commands
{
	public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, Order>
	{
		private readonly IRepository<Order> _repository;
		private readonly ILogger<PayOrderCommandHandler> _logger;

		public PayOrderCommandHandler(
			IRepository<Order> repository,
			ILogger<PayOrderCommandHandler> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<Order> Handle(PayOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var response = await _repository.UpdateAsync(request.Order);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Entity could not be updated");
				throw;
			}
		}
	}
}
