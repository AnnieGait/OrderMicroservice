using CustomerApi.Data.Repositories;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Sender;
using MediatR;

namespace CustomerApi.Service.Commands
{
	public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
	{
		private readonly ICustomerRepository<Customer> _repository;
		private readonly ICustomerUpdateSender _customerUpdateSender;

		public UpdateCustomerCommandHandler(ICustomerRepository<Customer> repository, ICustomerUpdateSender customerUpdateSender)
		{
			_repository = repository;
			_customerUpdateSender = customerUpdateSender;
		}

		public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
		{
			var customer = await _repository.UpdateAsync(request.Customer);

			_customerUpdateSender.SendCustomer(customer);

			return customer;
		}
	}
}
