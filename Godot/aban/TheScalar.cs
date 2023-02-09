﻿using Survival.aban.entities;
using System.Collections.Generic;
using System.Linq;

namespace Survival.aban;

public class TheScalar
{
    public readonly List<Entity> Entities = new();

    public void Tick()
    {
        Entities.ForEach(e =>
        {
            if (e is Character c)
            {
                c.Tick();
            }
        });
    }

    public Entity[] EntitiesAt(int location)
    {
        return Entities.Where(e => e.Location == location).ToArray();
    }

}