using OrderApi.Domain.Enums;

namespace OrderApi.Domain
{
	public partial class Order
	{
		public Guid Id { get; set; }

		public OrderState OrderState { get; set; }

		public Guid CustomerGuid { get; set; }

		public string CustomerFullName { get; set; }

		public void MarkOrderAsPaid()
		{
			OrderState = OrderState.Paid;
		}
	}
}
