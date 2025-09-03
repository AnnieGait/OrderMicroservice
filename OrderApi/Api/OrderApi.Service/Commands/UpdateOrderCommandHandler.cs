using MediatR;
using Microsoft.Extensions.Logging;
using OrderApi.Data.Repository;
using OrderApi.Domain;

namespace OrderApi.Service.Commands
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
	{
		private readonly IRepository<Order> _repository;
		private readonly ILogger<UpdateOrderCommandHandler> _logger;

		public UpdateOrderCommandHandler(
			IRepository<Order> repository,
			ILogger<UpdateOrderCommandHandler> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{

				await _repository.UpdateRangeAsync(request.Orders);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Entities could not be updated");
				throw;
			}
		}
	}
}
