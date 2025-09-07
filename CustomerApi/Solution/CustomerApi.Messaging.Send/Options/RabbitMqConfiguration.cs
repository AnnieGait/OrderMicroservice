namespace CustomerApi.Messaging.Send.Options
{
	public class RabbitMqConfiguration
	{
		public required string Hostname { get; set; }

		public required string QueueName { get; set; }

		public required string UserName { get; set; }

		public required string Password { get; set; }
	}
}
