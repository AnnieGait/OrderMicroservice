using MediatR;
using OrderApi.Data.Repository;
using OrderApi.Domain;

namespace OrderApi.Service.Commands
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
	{
		private readonly IRepository<Order> _repository;

		public UpdateOrderCommandHandler(IRepository<Order> repository)
		{
			_repository = repository;
		}

		public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			await _repository.UpdateRangeAsync(request.Orders);
		}

		// TODO : keep one method. Upgrade package
		Task<Unit> IRequestHandler<UpdateOrderCommand, Unit>.Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
