using System.Reflection;

namespace CleanArchitecture.Presentation;

public static class AssemblyPresentationReference
{
    public static readonly Assembly AssemblyPresentation = typeof(AssemblyPresentationReference).Assembly;
}