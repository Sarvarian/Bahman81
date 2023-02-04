using FirstPrototype.entities;
using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype;

public partial class Main : Node2D
{
    [Export] private PackedScene? characterScene_;
    [Export] private PackedScene? numberLabelScene_;
    [Export] private int pixelPerGroundRulerStep_ = 40;

    public override void _Ready()
    {
        base._Ready();

        Position = Vector2.Zero;

        this.AssertFiledSet(nameof(characterScene_));

        ConnectSignals();
        InitializeScreen();
        CreateGroundRuler();
        InstantiatePlayer();
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        base._UnhandledKeyInput(@event);
        @event.AssertType<InputEventKey>();
        inputHandler_.NewKeyInput((InputEventKey)@event);
    }

    private readonly InputHandler inputHandler_ = new();
    private readonly CoreGame.World world_ = new();
    private readonly CoreGame.Screen screen_ = new(0, 0);
    private Character? player_;

    private void InstantiatePlayer()
    {
        player_ = characterScene_!.Instantiate<Character>();
        AddChild(player_);
        player_.Position = new Vector2(screen_.CenterX, screen_.CenterY);
        player_.ConnectSignals(inputHandler_);
        player_.MoveRightCommand = MoveRightCommand;
        player_.MoveLeftCommand = MoveLeftCommand;
        player_.AttackCommand = AttackCommand;
    }

    private void ConnectSignals()
    {
        GetTree().Root.SizeChanged += OnRootSizeChanged;
    }

    private void InitializeScreen()
    {
        ResetScreenSize();
    }

    private void OnRootSizeChanged()
    {
        ResetScreenSize();
    }

    private void ResetScreenSize()
    {
        var oldSize = ScreenSizeAsVector2I();
        var newSize = ViewportSizeAsVector2I();
        if (oldSize != newSize)
        {
            screen_.NewSize(newSize.X, newSize.Y);
        }
    }

    private Vector2I ViewportSizeAsVector2I()
    {
        var res = new Vector2I();
        var size = GetTree().Root.GetVisibleRect().Size;
        res.X = (int)size.X;
        res.Y = (int)size.Y;
        return res;
    }

    private Vector2I ScreenSizeAsVector2I()
    {
        var res = new Vector2I
        {
            X = screen_.Width,
            Y = screen_.Height
        };
        return res;
    }

    private Vector2I ScreenCenterPointAsVector2I()
    {
        var res = new Vector2I
        {
            X = screen_.CenterX,
            Y = screen_.CenterY
        };
        return res;
    }

    private void CreateGroundRuler()
    {
        InstantiateANumberLabel(0);
        var maxStep = screen_.CenterX / pixelPerGroundRulerStep_;
        for (var i = 1; i < maxStep; i++)
        {
            InstantiateANumberLabel(i * 1);
            InstantiateANumberLabel(i * -1);
        }
    }

    private void InstantiateANumberLabel(int location)
    {
        var num = numberLabelScene_!.Instantiate<Number>();
        num.Position = ScreenCenterPointAsVector2I();
        num.Position += new Vector2(location * pixelPerGroundRulerStep_, 0);
        num.SetText($"{location}");
        AddChild(num);
    }

    private void MoveRightCommand()
    {
        world_.TickMoveRight();
    }

    private void MoveLeftCommand()
    {
        world_.TickMoveLeft();
    }

    private void AttackCommand()
    {
        // TODO: To be implemented.
    }

}