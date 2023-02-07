using Godot;

namespace SecondPrototype.nodes;

public partial class InputHandler : Node
{
    public static InputHandler Instantiate(Node parent)
    {
        var node = new InputHandler();
        parent.AddChild(node);
        return node;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);

    }
}