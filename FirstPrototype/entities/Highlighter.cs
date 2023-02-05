using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype.entities;

public partial class Highlighter : Node2D
{
    [Export] private Sprite2D? sprite2D_;
    [Export] private Color freePlaceColor_ = Colors.SteelBlue;
    [Export] private Color occupiedPlaceColor_ = Colors.DarkRed;

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

    public Vector2I GetSize()
    {
        return sprite2D_!.GetRect().Size.IntoGodotVector2I();
    }

    public void GoFreeColor()
    {
        sprite2D_!.Modulate = freePlaceColor_;
    }

    public void GoOccupiedColor()
    {
        sprite2D_!.Modulate = occupiedPlaceColor_;
    }

}