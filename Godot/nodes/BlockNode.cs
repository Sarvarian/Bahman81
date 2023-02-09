using Godot;
using Survival.extensions;

namespace Survival.nodes;

public partial class BlockNode : Node2D
{
    private static readonly StringName ScenePath = "res://scenes/block.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static BlockNode Instantiate(
        Node parent,
        aban.TheScalar scalar,
        aban.Grid2D grid,
        int location
    )
    {
        var node = Scene.Instantiate<BlockNode>();
        parent.AddChild(node);
        node.SetBlock(scalar, grid, location);
        return node;
    }

    [Export] private Sprite2D? sprite2D_;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(sprite2D_));
    }

    public void Remove()
    {
        scalar_!.Entities.Remove(block_!);
        QueueFree();
    }

    private void SetBlock(aban.TheScalar scalar, aban.Grid2D grid, int location)
    {
        block_ = new aban.entities.Block(location);
        Position = grid.LocationToPosition(location);
        scalar.Entities.Add(block_);
        scalar_ = scalar;
    }

    private aban.entities.Block? block_;
    private aban.TheScalar? scalar_;

}