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

    public void SetRightTrigger(Switch rightSwitch)
    {
        rightSwitch.ActionTrigger = OnTrigger;
#if DEBUG
        if ((rightSwitch.Location > Location) == false)
        {
            throw new WrongSide();
        }
#endif
    }

    public void SetLeftTrigger(Switch leftSwitch)
    {
        leftSwitch.ActionTrigger = OnTrigger;
#if DEBUG
        if ((leftSwitch.Location < Location) == false)
        {
            throw new WrongSide();
        }
#endif
    }

    private void OnTrigger()
    {

    }

}