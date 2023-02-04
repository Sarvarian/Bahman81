using System;

namespace FirstPrototype.exceptions;

public class UnexpectedType : Exception
{
    public UnexpectedType(string expectedTypeName, string actualTypeName)
        : base($"Expect of type {expectedTypeName}" +
               $" but got type {actualTypeName}.")
    {
    }
    
}