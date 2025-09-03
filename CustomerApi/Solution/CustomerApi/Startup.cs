using CustomerApi.Data.Database;
using CustomerApi.Data.Repositories;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Options;
using CustomerApi.Messaging.Send.Sender;
using CustomerApi.Models;
using CustomerApi.Service.Commands;
using CustomerApi.Service.Queries;
using CustomerApi.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;

namespace CustomerApi
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
			AddLogging(services);
			services.AddControllers();
			services.AddOptions();

			services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));

			services.AddDbContext<CustomerContext>(options =>
			{
				options.UseInMemoryDatabase(Guid.NewGuid().ToString());
				options.EnableSensitiveDataLogging();
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Customer Api",
					Description = "A simple API to create or update customers",
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
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());

			services.AddTransient(typeof(ICustomerRepository<>), typeof(CustomerRepository<>));

			services.AddTransient<IValidator<CreateCustomerModel>, CreateCustomerModelValidator>();
			services.AddTransient<IValidator<UpdateCustomerModel>, UpdateCustomerModelValidator>();

			services.AddTransient<ICustomerUpdateSender, CustomerUpdateSender>();

			services.AddTransient<IRequestHandler<CreateCustomerCommand, Customer>, CreateCustomerCommandHandler>();
			services.AddTransient<IRequestHandler<UpdateCustomerCommand, Customer>, UpdateCustomerCommandHandler>();
			services.AddTransient<IRequestHandler<GetCustomerByIdQuery, Customer>, GetCustomerByIdQueryHandler>();
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
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
			});
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private void AddLogging(IServiceCollection services)
		{
			services.AddLogging(_loggingBuilder =>
			{
				_loggingBuilder.ClearProviders();
				_loggingBuilder.AddNLog();
			});
		}
	}
}
