using CustomerApi.Domain.Entities;
using MediatR;

namespace CustomerApi.Service.Queries
{
	public class GetCustomerByIdQuery : IRequest<Customer>
	{
		public Guid Id { get; set; }
	}
}
