using FluentValidation;

namespace Infraestructure.Validations
{
    internal class CreateContactValidator : AbstractValidator<Domain.Contact>
    {
        public CreateContactValidator()
        {
            RuleFor(customer => customer.Uri)
                .Null()
                .WithMessage("Can't have an Uri when creating a contact");

            RuleFor(customer => customer.Birthdate)
                .Length(Domain.Contact.DateFormat.Length)
                .WithMessage("Set a valid date on Birthdate");
        }
    }
}
