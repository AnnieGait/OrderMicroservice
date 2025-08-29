using CustomerApi.Data.Repositories;
using CustomerApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Service.Queries
{
	public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
	{
		private readonly ICustomerRepository<Customer> _customerRepository;

		public GetCustomerByIdQueryHandler(ICustomerRepository<Customer> repository)
		{
			_customerRepository = repository;
		}

		public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
		{
			var result = await _customerRepository
				.GetAll()
				.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

			return result;
		}
	}
}
