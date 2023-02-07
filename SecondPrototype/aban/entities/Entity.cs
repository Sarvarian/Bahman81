using System;

namespace SecondPrototype.aban.entities;

public abstract class Entity
{
    public abstract void Tick();
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