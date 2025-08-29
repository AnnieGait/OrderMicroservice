using MediatR;
using OrderApi.Service.Commands;
using OrderApi.Service.Models;
using OrderApi.Service.Queries;

namespace OrderApi.Service.Services
{
	public class CustomerNameUpdateService : ICustomerNameUpdateService
	{
		private readonly IMediator _mediator;

		public CustomerNameUpdateService(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task UpdateCustomerNameInOrders(UpdateCustomerFullNameModel updateCustomerFullNameModel)
		{
			try
			{
				var ordersOfCustomer = await _mediator.Send(new GetOrderByCustomerGuidQuery
				{
					CustomerGuid = updateCustomerFullNameModel.Id
				});

				if (ordersOfCustomer.Count() != 0)
				{
					ordersOfCustomer.ForEach(x => x.CustomerFullName.Equals($"{updateCustomerFullNameModel.FirstName} {updateCustomerFullNameModel.LastName}"));

					await _mediator.Send(new UpdateOrderCommand
					{
						Orders = ordersOfCustomer
					});
				}
			}
			catch (Exception ex)
			{
				// TODO : Add nlog. log exception (e.g., using a logging framework)
				throw;
			}
		}
	}
}
