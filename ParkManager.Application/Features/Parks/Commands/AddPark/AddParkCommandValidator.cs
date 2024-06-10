using FluentValidation;

namespace ParkManager.Application.Features.Parks.Commands.AddPark
{
    public class AddParkCommandValidator : AbstractValidator<AddParkCommand>
    {
        public AddParkCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Street).NotEmpty();
            RuleFor(p => p.City).NotEmpty();
            RuleFor(p => p.State).NotEmpty();
            RuleFor(p => p.Country).NotEmpty();
            RuleFor(p => p.Latitude).NotEmpty();
            RuleFor(p => p.Longitude).NotEmpty();
        }
    }
}
