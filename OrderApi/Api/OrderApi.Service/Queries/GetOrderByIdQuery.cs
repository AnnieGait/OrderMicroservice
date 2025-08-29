using MediatR;
using OrderApi.Domain;

namespace OrderApi.Service.Queries
{
	public class GetOrderByIdQuery : IRequest<Order>
	{
		public Guid Id { get; set; }
	}
}
