using CustomerApi.Domain.Entities;

namespace CustomerApi.Messaging.Send.Sender
{
	public interface ICustomerUpdateSender
	{
		void SendCustomer(Customer customer);
	}
}
