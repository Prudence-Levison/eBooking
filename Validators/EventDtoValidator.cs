using FluentValidation;
using  eBooking.DTO;
 public class EventDtoValidator : AbstractValidator<CreateEventDTO>
 {
    public EventDtoValidator()
    {
        RuleFor (x => x.AvailableTickets)
        .NotEmpty()
        .WithMessage("Available Ticket must not be empty ");

        RuleFor(x => x.AvailableTickets)
        .GreaterThan(x => x.TotalTickets)
        .WithMessage("Avalable Tickets must be less than Total Tickets");

        // RuleFor(x => x.Date)
        // .LessThan(x => x.DateTime.Now)
        // .WithMessage("Event Date must be in the future");

        RuleFor(x => x.TotalTickets)
        .GreaterThan(0)
        .WithMessage("Toatl Tickets must be greater than zero");

        RuleFor(x => x.Title)
        .NotEmpty()
        .WithMessage("Title is required");

        RuleFor(x => x.Location)
        .NotEmpty()
        .WithMessage("Location is required");

        RuleFor(x => x.Cost)
        .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Description)
        .MaximumLength(1000)
        .WithMessage("Description must not exceed 1000 characters");

        
    }
 }