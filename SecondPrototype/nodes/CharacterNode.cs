using Godot;
using SecondPrototype.extensions;

namespace SecondPrototype.nodes;

public partial class CharacterNode : Node2D
{
    private static readonly StringName ScenePath = "res://scenes/character.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static CharacterNode Instantiate(
        Node parent,
        aban.TheScalar scalar,
        aban.Grid2D grid,
        Vector2I position
    )
    {
        var node = Scene.Instantiate<CharacterNode>();
        node.Prepare(scalar, grid, position);
        parent.AddChild(node);
        return node;
    }

    [Export] private Sprite2D? sprite2D_;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(sprite2D_));
    }

    private readonly aban.entities.Character character_ = new();

    private void Prepare(aban.TheScalar scalar, aban.Grid2D grid, Vector2I position)
    {
        scalar.Entities.Add(character_);
        character_.LocationChangedSignal += () => OnLocationChanged(grid);
        Position = position;
    }

    private void OnLocationChanged(aban.Grid2D grid)
    {
        Position = grid.LocationToPosition(character_.Location);
    }

}
