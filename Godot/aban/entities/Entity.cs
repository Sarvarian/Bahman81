using System;

namespace Survival.aban.entities;

public abstract class Entity
{
    public int Location { get; private set; }
    public Action? LocationChangedSignal;

    protected Entity()
    {
    }

    protected Entity(int location)
    {
        Location = location;
    }

    protected void NewLocation(int newLocation)
    {
        if (Location != newLocation)
        {
            Location = newLocation;
            LocationChangedSignal?.Invoke();
        }
    }
}