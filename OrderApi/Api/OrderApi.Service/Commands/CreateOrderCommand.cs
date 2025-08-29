using MediatR;
using OrderApi.Domain;

namespace OrderApi.Service.Commands
{
	public class CreateOrderCommand : IRequest<Order>
	{
		public Order Order { get; set; }
	}
}
