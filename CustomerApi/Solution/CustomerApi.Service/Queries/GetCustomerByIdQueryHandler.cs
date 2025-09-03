using CustomerApi.Data.Repositories;
using CustomerApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerApi.Service.Queries
{
	public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
	{
		private readonly ICustomerRepository<Customer> _customerRepository;
		private ILogger<GetCustomerByIdQueryHandler> _logger;

		public GetCustomerByIdQueryHandler(
			ICustomerRepository<Customer> repository,
			ILogger<GetCustomerByIdQueryHandler> logger)
		{
			_customerRepository = repository;
			_logger = logger;
		}

		public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _customerRepository
					.GetAll()
					.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Couldn't retrieve entities");
				throw;
			}
		}
	}
}
