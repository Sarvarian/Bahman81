using Godot;
using SecondPrototype.exceptions;
using System.Reflection;

namespace SecondPrototype.extensions;

public static class NodeExtension
{
    public static void AssertFiledSet(this Node node, string propertyName)
    {
#if DEBUG
        var type = node.GetType();
        const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
        var property = type.GetField(propertyName, flags)!;
        var value = property.GetValue(node);
        if (value == null)
        {
            throw new PropertyIsNotSet(propertyName, type.Name);
        }
#endif
    }
}