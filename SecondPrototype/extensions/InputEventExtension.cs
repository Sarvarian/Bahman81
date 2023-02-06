using Godot;
using SecondPrototype.exceptions;

namespace SecondPrototype.extensions;

public static class InputEventExtension
{
    public static void AssertType<T>(this InputEvent @event)
        where T : InputEvent
    {
#if DEBUG
        if (@event is not T)
        {
            var expectedTypeName = typeof(T).Name;
            var actualTypeName = @event.GetType().Name;
            throw new UnexpectedType(expectedTypeName, actualTypeName);
        }
#endif
    }
}