using AutoMapper;
using CleanArchitecture.Application.Features.Car.Commands.CreateCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;

namespace CleanArchitecture.Persistance.Services;

public class CarService(AppDbContext appDbContext, IMapper mapper) : ICarService
{
    public async Task CreateAsync(CreateCarCommand reqCarCommand, CancellationToken cancellationToken)
    {
        var car = mapper.Map<Car>(reqCarCommand);

        await appDbContext.AddAsync(car, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}