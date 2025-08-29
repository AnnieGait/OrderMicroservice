using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderApi.Data.Database;
using OrderApi.Data.Repository;
using OrderApi.Domain;
using OrderApi.Messaging.Receive.Options;
using OrderApi.Messaging.Receive.Receiver;
using OrderApi.Models;
using OrderApi.Service.Commands;
using OrderApi.Service.Queries;
using OrderApi.Service.Services;
using OrderApi.Validators;
using System.Reflection;

namespace OrderApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddOptions();

			services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));

			services.AddDbContext<OrderContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Order Api",
					Description = "A simple API to create or pay orders",
					Contact = new OpenApiContact
					{
						Name = "Anna Gaitanidi",
						Email = "gaitanidianna@gmail.com",
						Url = new Uri("https://github.com/AnnieGait")
					}
				});
			});

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = actionContext =>
				{
					var actionExecutingContext =
						actionContext as ActionExecutingContext;

					if (actionContext.ModelState.ErrorCount > 0
						&& actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
					{
						return new UnprocessableEntityObjectResult(actionContext.ModelState);
					}

					return new BadRequestObjectResult(actionContext.ModelState);
				};
			});

			services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ICustomerNameUpdateService).Assembly);

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			services.AddTransient<IValidator<OrderModel>, OrderModelValidator>();

			services.AddTransient<IRequestHandler<GetPaidOrderQuery, List<Order>>, GetPaidOrderQueryHandler>();
			services.AddTransient<IRequestHandler<GetOrderByIdQuery, Order>, GetOrderByIdQueryHandler>();
			services.AddTransient<IRequestHandler<GetOrderByCustomerGuidQuery, List<Order>>, GetOrderByCustomerGuidQueryHandler>();
			services.AddTransient<IRequestHandler<CreateOrderCommand, Order>, CreateOrderCommandHandler>();
			services.AddTransient<IRequestHandler<PayOrderCommand, Order>, PayOrderCommandHandler>();
			services.AddTransient<IRequestHandler<UpdateOrderCommand>, UpdateOrderCommandHandler>();
			services.AddTransient<ICustomerNameUpdateService, CustomerNameUpdateService>();

			services.AddHostedService<CustomerFullNameUpdateReceiver>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
			});
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}