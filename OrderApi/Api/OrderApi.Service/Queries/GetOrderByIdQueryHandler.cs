using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderApi.Data.Repository;
using OrderApi.Domain;

namespace OrderApi.Service.Queries
{
	public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
	{
		private readonly IRepository<Order> _repository;
		private readonly ILogger<GetOrderByIdQueryHandler> _logger;

		public GetOrderByIdQueryHandler(
			IRepository<Order> repository,
			ILogger<GetOrderByIdQueryHandler> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var order = await _repository
					.GetAll()
					.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

				return order;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Couldn't retrieve entity");
				throw;
			}
		}
	}
}
