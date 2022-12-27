using System;

namespace WMD.Game;

/// <summary>
/// An exception thrown when the type of an argument is a subclass of the base type accepted by the method, but not a supported subclass.
/// </summary>
/// <seealso cref="ArgumentException"/>
internal class UnsupportedArgumentSubclassException : ArgumentException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnsupportedArgumentSubclassException"/> class.
    /// </summary>
    /// <param name="baseType">The base type of the argument in the method signature.</param>
    /// <param name="argumentType">The actual type of the argument value supplied.</param>
    public UnsupportedArgumentSubclassException(Type baseType, Type argumentType) : base($"Unrecognized {baseType.Name} subclass: {argumentType.Name}.")
    {
        BaseType = baseType;
        ArgumentType = argumentType;
    }

    /// <summary>
    /// Gets the <see cref="Type"/> of the argument base class accepted by the method.
    /// </summary>
    public Type BaseType { get; }

    /// <summary>
    /// Gets the actual <see cref="Type"/> of the value supplied to the method.
    /// </summary>
    public Type ArgumentType { get; }
}
