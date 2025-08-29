using MediatR;
using OrderApi.Domain;

namespace OrderApi.Service.Queries
{
	public class GetPaidOrderQuery : IRequest<List<Order>>
	{
	}
}
