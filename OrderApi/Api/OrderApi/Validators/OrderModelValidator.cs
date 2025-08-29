using FluentValidation;
using OrderApi.Models;

namespace OrderApi.Validators
{
	public class OrderModelValidator : AbstractValidator<OrderModel>
	{
		public OrderModelValidator()
		{
			RuleFor(x => x.CustomerFullName)
				.NotNull()
				.WithMessage("Customer's name must be at least 2 characters long");

			RuleFor(x => x.CustomerFullName)
				.MinimumLength(2)
				.WithMessage("Customer's name must be at least 2 characters long");
		}
	}
}
