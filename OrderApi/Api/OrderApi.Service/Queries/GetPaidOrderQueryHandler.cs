using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Repository;
using OrderApi.Domain;
using OrderApi.Domain.Enums;

namespace OrderApi.Service.Queries
{
	public class GetPaidOrderQueryHandler : IRequestHandler<GetPaidOrderQuery, List<Order>>
	{
		private readonly IRepository<Order> _repository;

		public GetPaidOrderQueryHandler(IRepository<Order> repository)
		{
			_repository = repository;
		}

		public async Task<List<Order>> Handle(GetPaidOrderQuery request, CancellationToken cancellationToken)
		{
			var orders = await _repository
				.GetAll()
				.Where(x => x.OrderState == OrderState.Paid)
				.ToListAsync(cancellationToken);

			return orders;
		}
	}
}
