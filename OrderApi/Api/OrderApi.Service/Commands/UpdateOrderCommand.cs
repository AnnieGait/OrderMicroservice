using MediatR;
using OrderApi.Domain;

namespace OrderApi.Service.Commands
{
	public class UpdateOrderCommand : IRequest
	{
		public List<Order> Orders { get; set; }
	}
}
