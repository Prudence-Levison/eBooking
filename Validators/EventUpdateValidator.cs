using FluentValidation;
using eBooking.DTO;

public class EventUpdateValidator : AbstractValidator<UpdateEventDTO>
{
    public EventUpdateValidator()
    {
        When(x => x.AvailableTickets.HasValue, () =>
        {
            RuleFor(x => x.AvailableTickets.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Available Tickets must be a non-negative number");
        });

        When(x => x.AvailableSeats.HasValue, () =>
        {
            RuleFor(x => x.AvailableSeats.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Available Seats must be a non-negative number");
        });

        When(x => x.TotalTickets.HasValue, () =>
        {
            RuleFor(x => x.TotalTickets.Value)
                .GreaterThan(0)
                .WithMessage("Total Tickets must be greater than zero");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Title), () =>
        {
            RuleFor(x => x.Title)
                .MaximumLength(200)
                .WithMessage("Title is too long");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Location), () =>
        {
            RuleFor(x => x.Location)
                .MaximumLength(200)
                .WithMessage("Location is required and must be meaningful");
        });

        When(x => x.Cost.HasValue, () =>
        {
            RuleFor(x => x.Cost.Value)
                .GreaterThanOrEqualTo(0);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
        {
            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Description must not exceed 1000 characters");
        });

        When(x => x.Date.HasValue, () =>
        {
            RuleFor(x => x.Date.Value)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Event Date must be in the future");
        });
    }
}
