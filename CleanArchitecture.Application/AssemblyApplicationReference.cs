using System.Reflection;

namespace CleanArchitecture.Application;

public class AssemblyApplicationReference
{
    public static readonly Assembly Assembly = typeof(AssemblyApplicationReference).Assembly;
}