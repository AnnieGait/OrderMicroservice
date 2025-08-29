using CustomerApi.Models;
using FluentValidation;

namespace CustomerApi.Validators
{
	public class CreateCustomerModelValidator : AbstractValidator<CreateCustomerModel>
	{
		public CreateCustomerModelValidator()
		{
			RuleFor(x => x.FirstName)
				.NotNull()
				.WithMessage("First name must be at least 2 character long");
			RuleFor(x => x.FirstName)
				.MinimumLength(2).
				WithMessage("First name must be at least 2 character long");

			RuleFor(x => x.LastName)
				.NotNull()
				.WithMessage("Last name must be at least 2 character long");
			RuleFor(x => x.LastName)
				.MinimumLength(2)
				.WithMessage("Last name must be at least 2 character long");

			RuleFor(x => x.Birthday)
				.InclusiveBetween(DateTime.Now.AddYears(-150).Date, DateTime.Now)
				.WithMessage("Birthday has to be less than 150 years and can not be in the future");

			RuleFor(x => x.Age)
				.InclusiveBetween(0, 150)
				.WithMessage("Minimum age is 0 and maximum age is 150 years");
		}
	}
}
