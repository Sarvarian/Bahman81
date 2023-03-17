using Godot;

namespace Survival.nodes;

public abstract partial class EntityNode<T> : Node2D, aban.IEntityNode
    where T : aban.entities.Entity, new()
{
    bool aban.IEntityNode.IsActive()
    {
        return StateL1 == EStateL1.Active;
    }

    bool aban.IEntityNode.IsIdle()
    {
        return StateL1 == EStateL1.Idle;
    }

    protected enum EStateL1
    {
        Active,
        Idle,
    }
    protected EStateL1 StateL1 = EStateL1.Idle;
    protected T Entity = new();
    private aban.Grid2D grid_ = new(Vector2I.Zero);
    private int previousLocation_;
    private int currentLocation_;
    protected float ToCurrent = 1.0f;
    protected bool IsRequireMovement;

    protected void PrepareEntity(
        Vector2I position,
        T character,
        aban.Grid2D grid
    )
    {
        Entity = character;
        Entity.LocationChangedSignal -= OnLocationChanged;
        Entity.LocationChangedSignal += OnLocationChanged;
        Position = position;
        grid_ = grid;
        grid_.World!.OffsetUpdatedSignal -= OnWorldOffsetUpdated;
        grid_.World!.OffsetUpdatedSignal += OnWorldOffsetUpdated;
    }

    protected virtual void OnLocationChanged()
    {
        StateL1 = EStateL1.Active;
        IsRequireMovement = true;
        previousLocation_ = currentLocation_;
        currentLocation_ = Entity.Location;
        ToCurrent = 0.0f;
    }

    protected virtual void OnWorldOffsetUpdated()
    {
        Position = CalculatePosition();
    }

    protected virtual Vector2 CalculatePosition()
    {
        var current = grid_.LocationToPosition(currentLocation_);
        var previous = grid_.LocationToPosition(previousLocation_);
        var position = previous.Lerp(current, ToCurrent);
        return position;
    }

}