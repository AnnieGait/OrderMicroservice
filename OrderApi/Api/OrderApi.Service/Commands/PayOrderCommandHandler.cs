using MediatR;
using OrderApi.Data.Repository;
using OrderApi.Domain;

namespace OrderApi.Service.Commands
{
	public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, Order>
	{
		private readonly IRepository<Order> _repository;

		public PayOrderCommandHandler(IRepository<Order> repository)
		{
			_repository = repository;
		}

		public async Task<Order> Handle(PayOrderCommand request, CancellationToken cancellationToken)
		{
			var response = await _repository.UpdateAsync(request.Order);

			return response;
		}
	}
}
