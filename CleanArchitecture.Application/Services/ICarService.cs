using CleanArchitecture.Application.Features.Car.Commands.CreateCar;

namespace CleanArchitecture.Application.Services;

public interface ICarService
{
    Task CreateAsync(CreateCarCommand reqCarCommand, CancellationToken cancellationToken);
}