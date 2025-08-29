namespace OrderApi.Messaging.Receive.Options
{
	public class RabbitMqConfiguration
	{
		public required string Hostname { get; set; }

		public required string QueueName { get; set; }
	}
}