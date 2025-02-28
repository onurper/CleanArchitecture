using System.Reflection;

namespace CleanArchitecture.Persistance;

public static class AssemblyPersistanceReference
{
    public static readonly Assembly AssemblyPersistance = typeof(AssemblyPersistanceReference).Assembly;
}