using CustomerApi.Data.Repositories;
using CustomerApi.Domain.Entities;
using MediatR;

namespace CustomerApi.Service.Commands
{
	public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
	{
		private readonly ICustomerRepository<Customer> _repository;

		public CreateCustomerCommandHandler(ICustomerRepository<Customer> repository)
		{
			_repository = repository;
		}

		public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
		{
			return await _repository.AddAsync(request.Customer);
		}
	}
}

