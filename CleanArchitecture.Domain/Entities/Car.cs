using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Entities;

public sealed class Car : Entity
{
    public string Name { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int EnginePower { get; set; }
}