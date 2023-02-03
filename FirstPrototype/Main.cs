using Godot;

namespace FirstPrototype;

public partial class Main : Node2D
{
    public override void _Ready()
    {
        GD.Print(CoreGame.TestClass.TestString());

    }

}