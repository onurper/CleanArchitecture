using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.Car.Commands.CreateCar;

public sealed class CreateCarCommandHandler(ICarService carService) : IRequestHandler<CreateCarCommand, MessageResponse>
{
    public async Task<MessageResponse> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        await carService.CreateAsync(request, cancellationToken);
        return new MessageResponse("Car created successfully");
    }
}