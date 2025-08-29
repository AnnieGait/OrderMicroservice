using CustomerApi.Domain.Entities;
using CustomerApi.Models;
using Riok.Mapperly.Abstractions;

namespace CustomerApi.Infrastructure
{
	[Mapper]
	public partial class CustomerMapper
	{
		public partial Customer ToCostumer(CreateCustomerModel orderModel);

		public partial void UpdateCustomer(UpdateCustomerModel orderModel, Customer customer);
	}
}
