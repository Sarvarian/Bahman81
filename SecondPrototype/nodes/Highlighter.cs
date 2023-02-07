using Godot;
using SecondPrototype.extensions;

namespace SecondPrototype.nodes;

public partial class Highlighter : Node2D
{
    private static readonly StringName ScenePath = "res://scenes/highlighter.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static Highlighter Instantiate(Node parent)
    {
        var node = Scene.Instantiate<Highlighter>();
        parent.AddChild(node);
        node.Position = Vector2.Zero;
        return node;
    }

    [Export] private Sprite2D? sprite2D_;
    [Export] private Color freePlaceColor_ = Colors.SteelBlue;
    [Export] private Color occupiedPlaceColor_ = Colors.DarkRed;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(sprite2D_));
    }

    public Vector2I GetSize()
    {
        return sprite2D_!.GetRect().Size.ToVec2I();
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