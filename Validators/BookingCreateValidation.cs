using FluentValidation;
using eBooking.DTO;

namespace eBooking.Validators
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDTO>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0)
                .WithMessage("Event ID must be greater than 0.");

            RuleFor(x => x.NumberOfTickets)
                .GreaterThan(0)
                .WithMessage("Number of tickets must be greater than 0.");
        }
    }
}