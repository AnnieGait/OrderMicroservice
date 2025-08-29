using OrderApi.Service.Models;

namespace OrderApi.Service.Services
{
	public interface ICustomerNameUpdateService
	{
		Task UpdateCustomerNameInOrders(UpdateCustomerFullNameModel updateCustomerFullNameModel);
	}
}
