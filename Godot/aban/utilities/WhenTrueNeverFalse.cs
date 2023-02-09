namespace Survival.aban.utilities;

public struct WhenTrueNeverFalse
{
	public static implicit operator bool(WhenTrueNeverFalse data)
	{
		return data.value_;
	}

	public WhenTrueNeverFalse()
	{
	}

	public void MakeTrue()
	{
		value_ = true;
	}

	private bool value_ = false;
}