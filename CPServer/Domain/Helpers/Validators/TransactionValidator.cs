using FluentValidation;

namespace CPServer.Domain.Helpers.Validators;

public class TransactionValidator : AbstractValidator<CreateTransactionRequest>
{
	public TransactionValidator()
	{
		RuleFor(x => x.SenderId)
			.NotEmpty();

		RuleFor(x => x.RecipientId)
			.NotEmpty();

		RuleFor(x => x.Amount)
			.NotEmpty()
			.NotEqual("0")
			.NotEqual("0.0");
	}
}
