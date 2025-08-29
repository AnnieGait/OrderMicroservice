using MediatR;
using OrderApi.Data.Repository;
using OrderApi.Domain;

namespace OrderApi.Service.Commands
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
	{
		private readonly IRepository<Order> _repository;

		public CreateOrderCommandHandler(IRepository<Order> repository)
		{
			_repository = repository;
		}

		public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var response = await _repository.AddAsync(request.Order);

			return response;
		}
	}
}
