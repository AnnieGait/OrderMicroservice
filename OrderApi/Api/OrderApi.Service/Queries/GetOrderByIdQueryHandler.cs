using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Repository;
using OrderApi.Domain;

namespace OrderApi.Service.Queries
{
	public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
	{
		private readonly IRepository<Order> _repository;

		public GetOrderByIdQueryHandler(IRepository<Order> repository)
		{
			_repository = repository;
		}

		public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			var order = await _repository
				.GetAll()
				.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

			return order;
		}
	}
}
