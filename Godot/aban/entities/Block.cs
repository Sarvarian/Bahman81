using Survival.exceptions;

namespace Survival.aban.entities;

public class Block : Switch
{
    public Block(int location)
        : base(location)
    {
        if (location == 0)
        {
            throw new Location0();
        }
    }
}