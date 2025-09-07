using NLog;
using System.Reflection;

namespace CustomerApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var binPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var configPath = Path.Combine(binPath!, "nlog.config");

			Logger logger = LogManager.Setup().LoadConfigurationFromFile(configPath).GetCurrentClassLogger();

			try
			{
				logger.Info("Application is starting...");
				CreateWebHostBuilder(args).Build().Run();
			}
			catch (Exception exception)
			{
				logger.Error(exception, "Application is shutting down");
				throw;
			}
			finally
			{
				LogManager.Flush();
				LogManager.Shutdown();
			}
		}

		public static IHostBuilder CreateWebHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
		}
	}
}
