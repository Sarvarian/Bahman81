using Godot;
using SecondPrototype.extensions;

namespace SecondPrototype.nodes;

public partial class CharacterNode : Node2D
{
    private static readonly StringName ScenePath = "res://scenes/character.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static CharacterNode Instantiate(Node parent, Vector2I position)
    {
        var node = Scene.Instantiate<CharacterNode>();
        parent.AddChild(node);
        node.Position = position;
        return node;
    }

    [Export] private Sprite2D? sprite2D_;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(sprite2D_));
    }

}