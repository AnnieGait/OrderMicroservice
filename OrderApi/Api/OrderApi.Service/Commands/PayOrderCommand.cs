using MediatR;
using OrderApi.Domain;

namespace OrderApi.Service.Commands
{
	public class PayOrderCommand : IRequest<Order>
	{
		public Order Order { get; set; }
	}
}
