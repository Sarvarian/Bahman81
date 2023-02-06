namespace CoreGame.exceptions;

public class PixelPerGroundRulerStepIsZero : Exception
{
    public PixelPerGroundRulerStepIsZero()
        : base("Pixel per ground ruler is zero.")
    {
    }
}