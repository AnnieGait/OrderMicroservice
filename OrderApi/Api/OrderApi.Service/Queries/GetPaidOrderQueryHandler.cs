using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderApi.Data.Repository;
using OrderApi.Domain;
using OrderApi.Domain.Enums;

namespace OrderApi.Service.Queries
{
	public class GetPaidOrderQueryHandler : IRequestHandler<GetPaidOrderQuery, List<Order>>
	{
		private readonly IRepository<Order> _repository;
		private readonly ILogger<GetPaidOrderQueryHandler> _logger;

		public GetPaidOrderQueryHandler(
			IRepository<Order> repository,
			ILogger<GetPaidOrderQueryHandler> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<List<Order>> Handle(GetPaidOrderQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var orders = await _repository
					.GetAll()
					.Where(x => x.OrderState == OrderState.Paid)
					.ToListAsync(cancellationToken);

				return orders;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Couldn't retrieve entities");
				throw;
			}
		}
	}
}
