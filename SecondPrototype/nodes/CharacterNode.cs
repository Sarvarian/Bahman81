using Godot;
using SecondPrototype.aban;
using SecondPrototype.aban.entities;
using SecondPrototype.extensions;

namespace SecondPrototype.nodes;

public partial class CharacterNode : Node2D
{
    private static readonly StringName ScenePath = "res://scenes/character.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static CharacterNode Instantiate(Node parent, TheScalar scalar, Vector2I position)
    {
        var node = Scene.Instantiate<CharacterNode>();
        scalar.Entities.Add(node.character_);
        parent.AddChild(node);
        node.Position = position;
        return node;
    }

    [Export] private Sprite2D? sprite2D_;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(sprite2D_));
    }

    private readonly Character character_ = new();

}
