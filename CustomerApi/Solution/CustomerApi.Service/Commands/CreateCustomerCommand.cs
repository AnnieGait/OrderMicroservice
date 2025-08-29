using CustomerApi.Domain.Entities;
using MediatR;

namespace CustomerApi.Service.Commands
{
	public class CreateCustomerCommand : IRequest<Customer>
	{
		public required Customer Customer { get; set; }
	}
}
