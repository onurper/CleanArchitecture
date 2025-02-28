using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.Car.Commands.CreateCar;

public sealed class CreateCarCommandHandler(ICarService carService) : IRequestHandler<CreateCarCommand, MessageResponse>
{
    public Task<MessageResponse> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        carService.CreateAsync(request, cancellationToken);
        return Task.FromResult(new MessageResponse("Car created successfully"));
    }
}