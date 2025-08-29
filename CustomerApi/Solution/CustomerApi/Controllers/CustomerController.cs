using CustomerApi.Domain.Entities;
using CustomerApi.Infrastructure;
using CustomerApi.Models;
using CustomerApi.Service.Commands;
using CustomerApi.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
	[Produces("application/json")]
	[Route("v1/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly CustomerMapper _mapper;

		public CustomerController(IMediator mediator)
		{
			_mapper = new CustomerMapper();
			_mediator = mediator;
		}

		/// <summary>
		/// Action to create a new customer in the database.
		/// </summary>
		/// <param name="createCustomerModel">Model to create a new customer</param>
		/// <returns>Returns the created customer</returns>
		/// /// <response code="200">Returned if the customer was created</response>
		/// /// <response code="400">Returned if the model couldn't be parsed or the customer couldn't be saved</response>
		/// /// <response code="422">Returned when the validation failed</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[HttpPost]
		public async Task<ActionResult<Customer>> Create([FromBody] CreateCustomerModel createCustomerModel)
		{
			try
			{
				var command = new CreateCustomerCommand
				{
					Customer = _mapper.ToCostumer(createCustomerModel)
				};
				var response = await _mediator.Send(command);
				return response;
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Action to update an existing customer
		/// </summary>
		/// <param name="updateCustomerModel">Model to update an existing customer</param>
		/// <returns>Returns the updated customer</returns>
		/// /// <response code="200">Returned if the customer was updated</response>
		/// /// <response code="400">Returned if the model couldn't be parsed or the customer couldn't be found</response>
		/// /// <response code="422">Returned when the validation failed</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[HttpPut]
		public async Task<ActionResult<Customer>> Update([FromBody] UpdateCustomerModel updateCustomerModel)
		{
			try
			{
				var getCustomerQuery = new GetCustomerByIdQuery
				{
					Id = updateCustomerModel.Id
				};
				var customer = await _mediator.Send(getCustomerQuery);

				if (customer == null)
					return BadRequest($"No customer found with the id {updateCustomerModel.Id}");


				_mapper.UpdateCustomer(updateCustomerModel, customer);
				return await _mediator.Send(new UpdateCustomerCommand
				{
					Customer = customer
				});
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
