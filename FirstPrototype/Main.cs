using FirstPrototype.entities;
using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype;

public partial class Main : Node2D
{
    [Export] private PackedScene? highlighterScene_;
    [Export] private PackedScene? numberLabelScene_;
    [Export] private PackedScene? characterScene_;
    [Export] private PackedScene? blockScene_;

    public override void _Ready()
    {
        base._Ready();

        Position = Vector2.Zero;

        this.AssertFiledSet(nameof(highlighterScene_));
        this.AssertFiledSet(nameof(numberLabelScene_));
        this.AssertFiledSet(nameof(characterScene_));
        this.AssertFiledSet(nameof(blockScene_));

        ConnectSignals();
        InitializeScreen();
        InstantiateHighlighter();
        CreateGroundRuler();
        InstantiatePlayer();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        inputHandler_.NewMousePosition(GetGlobalMousePosition());

        UpdateHighlighterPosition();

        // TODO: Extract a method out of this if block.
        if (player_!.IsGotInputEvent)
        {
            world_.Tick();
            UpdatePlayerLocation();
            player_.IsGotInputEvent = false;
        }
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
    private Highlighter? highlighter_;
    private int pixelPerGroundRulerStep_;

    private void InstantiatePlayer()
    {
        player_ = characterScene_!.Instantiate<Character>();
        AddChild(player_);
        player_.Position = new Vector2(screen_.CenterX, screen_.CenterY);
        player_.ConnectSignals(inputHandler_);
        player_.SetCoreCharacter(world_.Player);
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

    private void UpdatePlayerLocation()
    {
        player_!.UpdateLocation(ScreenCenterPointAsVector2I(), pixelPerGroundRulerStep_);
    }

    private void InstantiateHighlighter()
    {
        highlighter_ = highlighterScene_!.Instantiate<Highlighter>();
        pixelPerGroundRulerStep_ = highlighter_.GetWidth();
        AddChild(highlighter_);
        highlighter_.Position = Vector2.Zero;
    }

    private bool IsMouseCloseAndAboveGround()
    {
        var mousePos = GetGlobalMousePosition();
        var mouseY = mousePos.Y;
        var maxY = screen_.CenterY;
        var minY = maxY - highlighter_!.GetHeight();
        return mouseY > minY && mouseY < maxY;
    }

    private Vector2 WhereToPutHighlighter()
    {
        if (IsMouseCloseAndAboveGround() == false)
        {
            return Vector2.Zero;
        }

        var res = Vector2.Zero;
        res.Y = screen_.CenterY;

        var mousePos = GetGlobalMousePosition();
        var mouseX = Mathf.RoundToInt(mousePos.X);
        var distanceToCenter = mouseX - screen_.CenterX;
        var rulerPoint =
            Mathf.RoundToInt((float)distanceToCenter / (float)pixelPerGroundRulerStep_);
        res.X = screen_.CenterX + (rulerPoint * pixelPerGroundRulerStep_);

        return res;
    }

    private void UpdateHighlighterPosition()
    {
        highlighter_!.Position = WhereToPutHighlighter();
    }

    public override void _Draw()
    {
        base._Draw();

        var color = Colors.DarkRed;

        DrawLine(
            new Vector2(0, screen_.CenterY),
            new Vector2(screen_.Width, screen_.CenterY),
            color
        );

        DrawLine(
            new Vector2(screen_.CenterX, screen_.CenterY),
            new Vector2(screen_.CenterX, screen_.CenterY + 10),
            color
        );

        var maxStep = screen_.CenterX / pixelPerGroundRulerStep_;
        for (var i = 1; i < maxStep; i++)
        {
            var xp = screen_.CenterX + (i * 1 * pixelPerGroundRulerStep_);
            DrawLine(
                new Vector2(xp, screen_.CenterY),
                new Vector2(xp, screen_.CenterY + 10),
                color
            );
            var xn = screen_.CenterX + (i * -1 * pixelPerGroundRulerStep_);
            DrawLine(
                new Vector2(xn, screen_.CenterY),
                new Vector2(xn, screen_.CenterY + 10),
                color
            );
        }
    }
}