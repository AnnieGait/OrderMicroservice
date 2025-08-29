using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Repository;
using OrderApi.Domain;

namespace OrderApi.Service.Queries
{
	public class GetOrderByCustomerGuidQueryHandler : IRequestHandler<GetOrderByCustomerGuidQuery, List<Order>>
	{
		private readonly IRepository<Order> _repository;
		public GetOrderByCustomerGuidQueryHandler(IRepository<Order> repository)
		{
			_repository = repository;
		}

		public async Task<List<Order>> Handle(GetOrderByCustomerGuidQuery request, CancellationToken cancellationToken)
		{
			var orders = await _repository
				.GetAll()
				.Where(x => x.CustomerGuid == request.CustomerGuid)
				.ToListAsync(cancellationToken);

			return orders;
		}
	}
}
