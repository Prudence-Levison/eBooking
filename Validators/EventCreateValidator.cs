using FluentValidation;
using  eBooking.DTO;
 public class EventCreateValidator : AbstractValidator<CreateEventDTO>
 {
    public EventCreateValidator()
    {
        RuleFor (x => x.AvailableTickets)
        .NotEmpty()
        .WithMessage("Available Ticket must not be empty ");

        RuleFor(x => x.AvailableSeats)
        .GreaterThanOrEqualTo(0)
        .WithMessage("Available Seats must be a non-negative number");

        RuleFor(x => x.AvailableTickets)
        .LessThanOrEqualTo(x => x.TotalTickets)
        .WithMessage("Available Tickets must be less than or equal to Total Tickets");

        RuleFor(x => x.Date)
        .GreaterThan(DateTime.UtcNow)
        .WithMessage("Event Date must be in the future");

        RuleFor(x => x.TotalTickets)
        .GreaterThan(0)
        .WithMessage("Total Tickets must be greater than zero");

        RuleFor(x => x.AvailableTickets)
        .GreaterThanOrEqualTo(0)
        .WithMessage("Available Tickets must be a non-negative number");

        RuleFor(x => x.Title)
        .NotEmpty()
        .Must(x => x.ToLower() != "string")
        .WithMessage("Title is required and must be meaningful");

        RuleFor(x => x.Location)
        .NotEmpty()
        .Must(x => x.ToLower() != "string")
        .WithMessage("Location is required and must be meaningful");

        RuleFor(x => x.Cost)
        .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Description)
        .MaximumLength(1000)
        .Must(x => x.ToLower() != "string")
        .WithMessage("Description must not exceed 1000 characters and must be meaningful");     
    }
 }