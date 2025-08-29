using MediatR;
using OrderApi.Domain;

namespace OrderApi.Service.Queries
{
	public class GetOrderByCustomerGuidQuery : IRequest<List<Order>>
	{
		public Guid CustomerGuid { get; set; }
	}
}
