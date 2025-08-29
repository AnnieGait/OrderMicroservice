using CustomerApi.Domain.Entities;
using MediatR;

namespace CustomerApi.Service.Commands
{
	public class UpdateCustomerCommand : IRequest<Customer>
	{
		public required Customer Customer { get; set; }
	}
}
