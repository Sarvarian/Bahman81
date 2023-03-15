using Godot;
using Survival.extensions;

namespace Survival.nodes;

public partial class CharacterNode : Node2D, aban.IEntityNode
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
        base._Ready();
        this.AssertFiledSet(nameof(sprite2D_));
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        MoveIfRequireMovement(delta);
        CheckIfActivityIsDone();
    }

    public void ConnectSignals(no_use_in_editor.InputHandler handler)
    {
        handler.MoveRightSignal -= MoveRight;
        handler.MoveRightSignal += MoveRight;
        handler.MoveLeftSignal -= MoveLeft;
        handler.MoveLeftSignal += MoveLeft;
        handler.AttackSignal -= Attack;
        handler.AttackSignal += Attack;
        // We remove and then add signals just to prevent duplication.
    }

    public void ConnectWorldOffset(aban.GameWorld world)
    {
        world.OffsetUpdatedSignal += OnWorldOffsetUpdated;
    }

    bool aban.IEntityNode.IsActive()
    {
        return stateL1_ == StateL1.Active;
    }

    bool aban.IEntityNode.IsIdle()
    {
        return stateL1_ == StateL1.Idle;
    }

    private enum StateL1
    {
        Active,
        Idle,
    }

    private readonly aban.entities.Character character_ = new();
    private aban.Grid2D? grid_;
    private StateL1 stateL1_ = StateL1.Idle;
    private bool isRequireMovement_;
    private int previousLocation_;
    private int currentLocation_;
    private float toCurrent_ = 1.0f;

    private void Prepare(
        Vector2I position,
        aban.TheScalar scalar,
        aban.Grid2D grid
    )
    {
        scalar.Entities.Add(character_);
        character_.LocationChangedSignal += OnLocationChanged;
        Position = position;
        grid_ = grid;
    }

    private void OnLocationChanged()
    {
        stateL1_ = StateL1.Active;
        isRequireMovement_ = true;
        previousLocation_ = currentLocation_;
        currentLocation_ = character_.Location;
        toCurrent_ = 0.0f;
    }

    private void OnWorldOffsetUpdated()
    {
        Position = CalculatePosition();
    }

    private void MoveRight()
    {
        if (stateL1_ == StateL1.Idle)
        {
            character_.SetToMoveRight();
            IsGotInputEvent = true;
        }
    }

    private void MoveLeft()
    {
        if (stateL1_ == StateL1.Idle)
        {
            character_.SetToMoveLeft();
            IsGotInputEvent = true;
        }
    }

    private void Attack()
    {
        if (stateL1_ == StateL1.Idle)
        {
            IsGotInputEvent = true;
            // TODO: To Be Implemented.
        }
    }

    private void MoveIfRequireMovement(double delta)
    {
        if (isRequireMovement_)
        {
            if (toCurrent_ < 1.0f)
            {
                toCurrent_ += (float)(2.5d * delta);
                Position = CalculatePosition();
            }
        }
    }

    private Vector2 CalculatePosition()
    {
        var current = grid_!.LocationToPosition(currentLocation_);
        var previous = grid_!.LocationToPosition(previousLocation_);
        var position = previous.Lerp(current, toCurrent_);
        return position;
    }

    private void CheckIfActivityIsDone()
    {
        if (stateL1_ == StateL1.Active)
        {
            if (isRequireMovement_)
            {
                if (toCurrent_ >= 1.0f)
                {
                    isRequireMovement_ = false;
                    stateL1_ = StateL1.Idle;
                }
            }
        }
    }

}
