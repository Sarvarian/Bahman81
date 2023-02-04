using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype.entities;

public partial class Highlighter : Node2D
{
    [Export] private Sprite2D? sprite2D_;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(sprite2D_));
    }

    public int GetWidth()
    {
        return (int)sprite2D_!.GetRect().Size.X;
    }

    public int GetHeight()
    {
        return (int)sprite2D_!.GetRect().Size.Y;
    }

}