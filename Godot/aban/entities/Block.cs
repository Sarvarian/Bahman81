using Survival.aban.utilities;
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
#if DEBUG
		if (isRightTriggerSet_)
		{
			throw new MultipleSetTrigger();
		}

		if ((rightSwitch.Location > Location) == false)
		{
			throw new WrongSide();
		}
#endif
		rightSwitch.ActionTrigger = OnTrigger;
		isRightTriggerSet_.MakeTrue();
	}

	public void SetLeftTrigger(Switch leftSwitch)
	{
#if DEBUG
		if (isLeftTriggerSet_)
		{
			throw new MultipleSetTrigger();
		}

		if ((leftSwitch.Location < Location) == false)
		{
			throw new WrongSide();
		}
#endif
		leftSwitch.ActionTrigger = OnTrigger;
		isLeftTriggerSet_.MakeTrue();
	}

	private WhenTrueNeverFalse isRightTriggerSet_;
	private WhenTrueNeverFalse isLeftTriggerSet_;

	private void OnTrigger()
	{

	}

}