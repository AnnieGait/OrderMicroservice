using FluentValidation.TestHelper;
using OrderApi.Validators;

namespace OrderApi.Test.Validators
{
	public class OrderModelValidatorTests
	{
		private readonly OrderModelValidator _testee;

		public OrderModelValidatorTests()
		{
			_testee = new OrderModelValidator();
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		[InlineData("a")]
		public void CustomerFullName_WhenShorterThanTwoCharacter_ShouldHaveValidationError(string customerFullName)
		{
			_testee.ShouldHaveValidationErrorFor(x => x.CustomerFullName, customerFullName).WithErrorMessage("Customer's name must be at least 2 characters long");
		}

		[Fact]
		public void CustomerFullName_WhenLongerThanTwoCharacter_ShouldNotHaveValidationError()
		{
			_testee.ShouldNotHaveValidationErrorFor(x => x.CustomerFullName, "Ab");
		}
	}
}
