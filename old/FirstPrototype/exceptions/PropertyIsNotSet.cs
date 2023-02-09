using System;

namespace FirstPrototype.exceptions;

public class PropertyIsNotSet : Exception
{
    public PropertyIsNotSet(string propertyName, string className)
        : base($"Property {propertyName}" +
               $" in class {className}" +
               $" is not set.")
    {
    }
}