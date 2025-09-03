using CustomerApi.Data.Repositories;
using CustomerApi.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerApi.Service.Commands
{
	public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
	{
		private readonly ICustomerRepository<Customer> _repository;
		private readonly ILogger<CreateCustomerCommandHandler> _logger;

		public CreateCustomerCommandHandler(
			ICustomerRepository<Customer> repository,
			ILogger<CreateCustomerCommandHandler> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
		{
			try
			{

				return await _repository.AddAsync(request.Customer);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Customer could not be saved");
				throw;
			}
		}
	}
}

