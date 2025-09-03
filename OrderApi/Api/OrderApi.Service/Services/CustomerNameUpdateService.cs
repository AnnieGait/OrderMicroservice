using MediatR;
using Microsoft.Extensions.Logging;
using OrderApi.Service.Commands;
using OrderApi.Service.Models;
using OrderApi.Service.Queries;

namespace OrderApi.Service.Services
{
	public class CustomerNameUpdateService : ICustomerNameUpdateService
	{
		private readonly IMediator _mediator;
		private readonly ILogger<CustomerNameUpdateService> _logger;

		public CustomerNameUpdateService(
			IMediator mediator,
			ILogger<CustomerNameUpdateService> logger)
		{
			_mediator = mediator;
			_logger = logger;
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
				_logger.LogError(ex, "Couldn't update customer full name in orders");
				throw;
			}
		}
	}
}
