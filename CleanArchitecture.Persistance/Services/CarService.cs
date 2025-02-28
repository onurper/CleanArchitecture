using CleanArchitecture.Application.Features.Car.Commands.CreateCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;

namespace CleanArchitecture.Persistance.Services;

public class CarService(AppDbContext appDbContext) : ICarService
{
    public async Task CreateAsync(CreateCarCommand reqCarCommand, CancellationToken cancellationToken)
    {
        var car = new Car
        {
            Name = reqCarCommand.Name,
            Model = reqCarCommand.Model,
            EnginePower = reqCarCommand.EnginePower
        };

        await appDbContext.AddAsync(car, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}