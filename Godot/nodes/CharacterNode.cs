using Godot;
using Survival.extensions;

namespace Survival.nodes;

public partial class CharacterNode : EntityNode<aban.entities.Character>
{
    private static readonly StringName ScenePath = "res://scenes/character.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static CharacterNode Instantiate(Node parent,
        Vector2I position,
        aban.entities.Character character,
        aban.Grid2D grid
    )
    {
        var node = Scene.Instantiate<CharacterNode>();
        node.PrepareEntity(position, character, grid);
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

    private aban.entities.Character Character => Entity;

    private void MoveRight()
    {
        if (StateL1 == EStateL1.Idle)
        {
            Character.SetToMoveRight();
            IsGotInputEvent = true;
        }
    }

    private void MoveLeft()
    {
        if (StateL1 == EStateL1.Idle)
        {
            Character.SetToMoveLeft();
            IsGotInputEvent = true;
        }
    }

    private void Attack()
    {
        if (StateL1 == EStateL1.Idle)
        {
            IsGotInputEvent = true;
            // TODO: To Be Implemented.
        }
    }

    private void MoveIfRequireMovement(double delta)
    {
        if (IsRequireMovement)
        {
            if (ToCurrent < 1.0f)
            {
                ToCurrent += (float)(5.0d * delta);
                Position = CalculatePosition();
            }
        }
    }

    private void CheckIfActivityIsDone()
    {
        if (StateL1 == EStateL1.Active)
        {
            if (IsRequireMovement)
            {
                if (ToCurrent >= 1.0f)
                {
                    IsRequireMovement = false;
                    StateL1 = EStateL1.Idle;
                }
            }
        }
    }

}
