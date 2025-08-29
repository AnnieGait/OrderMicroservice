using OrderApi.Domain;
using OrderApi.Models;
using Riok.Mapperly.Abstractions;

namespace OrderApi.Infrastructure
{
	[Mapper]
	public partial class OrderMapper
	{
		public partial Order OrderToOrderDto(OrderModel orderModel);
	}
}
