using System;

namespace Survival.aban.entities;

public class Switch : Entity
{
    public Action? ActionTrigger { private get; set; }

    public Switch(int location)
        : base(location)
    {
    }

    public void DoSwitch()
    {
        ActionTrigger?.Invoke();
    }
}