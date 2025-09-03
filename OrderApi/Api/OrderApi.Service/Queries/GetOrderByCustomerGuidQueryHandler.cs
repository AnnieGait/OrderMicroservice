using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderApi.Data.Repository;
using OrderApi.Domain;

namespace OrderApi.Service.Queries
{
	public class GetOrderByCustomerGuidQueryHandler : IRequestHandler<GetOrderByCustomerGuidQuery, List<Order>>
	{
		private readonly IRepository<Order> _repository;
		private readonly ILogger<GetOrderByCustomerGuidQueryHandler> _logger;

		public GetOrderByCustomerGuidQueryHandler(
			IRepository<Order> repository,
			ILogger<GetOrderByCustomerGuidQueryHandler> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<List<Order>> Handle(GetOrderByCustomerGuidQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var orders = await _repository
					.GetAll()
					.Where(x => x.CustomerGuid == request.CustomerGuid)
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
