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
    protected aban.Grid2D Grid = new(Vector2I.Zero);

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
        Grid = grid;
        Grid.World!.OffsetUpdatedSignal -= OnWorldOffsetUpdated;
        Grid.World!.OffsetUpdatedSignal += OnWorldOffsetUpdated;
    }

    protected abstract void OnLocationChanged();
    protected abstract void OnWorldOffsetUpdated();

}