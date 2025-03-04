using CleanArchitecture.Application.Features.Car.Commands.CreateCar;
using CleanArchitecture.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

public class CarsController(IMediator mediator) : ApiController(mediator)
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateCarAsync(CreateCarCommand reqCarCommand, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(reqCarCommand, cancellationToken);
        return Ok(response);
    }
}