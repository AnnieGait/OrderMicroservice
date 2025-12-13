using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderApi.Service.Commands;
using OrderApi.Service.Models;
using OrderApi.Service.Queries;

namespace OrderApi.Service.Services
{
	public class CustomerNameUpdateService : ICustomerNameUpdateService
	{
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly ILogger<CustomerNameUpdateService> _logger;

		public CustomerNameUpdateService(
			IServiceScopeFactory scopeFactory,
			ILogger<CustomerNameUpdateService> logger)
		{
			_scopeFactory = scopeFactory;
			_logger = logger;
		}

		public async Task UpdateCustomerNameInOrders(UpdateCustomerFullNameModel updateCustomerFullNameModel)
		{
			try
			{
				using (var scope = _scopeFactory.CreateScope())
				{
					var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

					var ordersOfCustomer = await _mediator.Send(new GetOrderByCustomerGuidQuery
					{
						CustomerGuid = updateCustomerFullNameModel.Id
					});

					if (ordersOfCustomer.Any())
					{
						foreach (var order in ordersOfCustomer)
						{
							order.CustomerFullName = $"{updateCustomerFullNameModel.FirstName} {updateCustomerFullNameModel.LastName}";
						}

						await _mediator.Send(new UpdateOrderCommand
						{
							Orders = ordersOfCustomer
						});
					}
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
