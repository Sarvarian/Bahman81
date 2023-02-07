using Godot;
using SecondPrototype.extensions;

namespace SecondPrototype.nodes;

public partial class CharacterNode : Node2D
{
    private static readonly StringName ScenePath = "res://scenes/character.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static CharacterNode Instantiate(Node parent,
        Vector2I position,
        aban.TheScalar scalar,
        aban.Grid2D grid
    )
    {
        var node = Scene.Instantiate<CharacterNode>();
        node.Prepare(position, scalar, grid);
        parent.AddChild(node);
        return node;
    }

    [Export] private Sprite2D? sprite2D_;
    public bool IsGotInputEvent;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(sprite2D_));
    }

    public void ConnectSignals(InputHandler handler)
    {
        handler.MoveRightSignal -= MoveRight;
        handler.MoveRightSignal += MoveRight;
        handler.MoveLeftSignal -= MoveLeft;
        handler.MoveLeftSignal += MoveLeft;
        handler.AttackSignal -= Attack;
        handler.AttackSignal += Attack;
        // We remove and then add signals just to prevent duplication.
    }

    private readonly aban.entities.Character character_ = new();

    private void Prepare(
        Vector2I position,
        aban.TheScalar scalar,
        aban.Grid2D grid
    )
    {
        scalar.Entities.Add(character_);
        character_.LocationChangedSignal += () => OnLocationChanged(grid);
        Position = position;
    }

    private void OnLocationChanged(aban.Grid2D grid)
    {
        Position = grid.LocationToPosition(character_.Location);
    }

    private void MoveRight()
    {
        character_.SetToMoveRight();
        IsGotInputEvent = true;
    }

    private void MoveLeft()
    {
        character_.SetToMoveLeft();
        IsGotInputEvent = true;
    }

    private void Attack()
    {
        IsGotInputEvent = true;
        // TODO: To Be Implemented.
    }

}
