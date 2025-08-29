using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Domain;
using OrderApi.Infrastructure;
using OrderApi.Models;
using OrderApi.Service.Commands;
using OrderApi.Service.Queries;

namespace OrderApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly OrderMapper _mapper;

		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
			_mapper = new OrderMapper();
		}

		/// <summary>
		///     Action to create a new order in the database.
		/// </summary>
		/// <param name="orderModel">Model to create a new order</param>
		/// <returns>Returns the created order</returns>
		/// <response code="200">Returned if the order was created</response>
		/// <response code="400">Returned if the model couldn't be parsed or saved</response>
		/// <response code="422">Returned when the validation failed</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[HttpPost]
		public async Task<ActionResult<Order>> Create([FromBody] OrderModel orderModel)
		{
			try
			{
				var command = new CreateOrderCommand
				{
					Order = _mapper.OrderToOrderDto(orderModel)
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
		///     Action to retrieve all pay orders.
		/// </summary>
		/// <returns>Returns a list of all paid orders or an empty list, if no order is paid yet</returns>
		/// <response code="200">Returned if the list of orders was retrieved</response>
		/// <response code="400">Returned if the orders could not be retrieved</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpGet]
		public async Task<ActionResult<List<Order>>> Get()
		{
			try
			{
				var response = await _mediator.Send(new GetPaidOrderQuery());
				return response;
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		///     Action to pay an order.
		/// </summary>
		/// <param name="id">The id of the order which got paid</param>
		/// <returns>Returns the paid order</returns>
		/// <response code="200">Returned if the order was updated (paid)</response>
		/// <response code="400">Returned if the order could not be found with the provided id</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPut("pay/{id}")]
		public async Task<ActionResult<Order>> Pay(Guid id)
		{
			try
			{
				Order order = await _mediator.Send(new GetOrderByIdQuery
				{
					Id = id
				});

				if (order == null)
					return BadRequest($"No order found with the id {id}");

				order.MarkOrderAsPaid();

				var response = await _mediator.Send(new PayOrderCommand
				{
					Order = order
				});
				return response;
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

