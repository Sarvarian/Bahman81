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
        UpdateHighlighter();
        DoTheTick();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledKeyInput(@event);
        inputHandler_.NewInput(@event);
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
        inputHandler_.ImplantEntitySignal += OnInputHandlerImplantEntitySignal;
        inputHandler_.RemoveEntitySignal += OnInputHandlerRemoveEntitySignal;
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

    private void DoTheTick()
    {
        if (player_!.IsGotInputEvent)
        {
            world_.Tick();
            UpdatePlayerLocation();
            player_.IsGotInputEvent = false;
        }
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
        var minY = screen_.CenterY - highlighter_!.GetHeight();
        var maxY = screen_.CenterY + highlighter_!.GetHeight();
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
            Mathf.RoundToInt(distanceToCenter / (float)pixelPerGroundRulerStep_);
        res.X = screen_.CenterX + (rulerPoint * pixelPerGroundRulerStep_);

        return res;
    }

    private bool IsHighlighterInactive()
    {
        return highlighter_!.Position == Vector2.Zero;
    }

    private int HighlighterLocation()
    {
        return ((int)highlighter_!.Position.X - screen_.CenterX) / pixelPerGroundRulerStep_;
    }

    private void UpdateHighlighter()
    {
        highlighter_!.Position = WhereToPutHighlighter();
        if (world_.EntitiesAt(HighlighterLocation()).Length == 0)
        {
            highlighter_.GoFreeColor();
        }
        else
        {
            highlighter_.GoOccupiedColor();
        }
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

    private void OnInputHandlerImplantEntitySignal()
    {
        if (IsHighlighterInactive()) { return; }

        var location = HighlighterLocation();
        if (world_.EntitiesAt(location).Length == 0)
        {
            InstantiateBlockAt(location);
        }
    }

    private void OnInputHandlerRemoveEntitySignal()
    {
        if (IsHighlighterInactive()) { return; }

        var location = HighlighterLocation();
        foreach (var entity in world_.EntitiesAt(location))
        {
            if (entity is CoreGame.Block)
            {
                RemoveBlockEntityAt(location);
            }
        }
    }

    private void InstantiateBlockAt(int location)
    {
        var block = blockScene_!.Instantiate<Block>();
        var pos = new Vector2
        {
            Y = screen_.CenterY,
            X = screen_.CenterX + (location * pixelPerGroundRulerStep_)
        };
        AddChild(block);
        block.GlobalPosition = pos;
        var coreBlock = new CoreGame.Block(location);
        world_.Entities.Add(coreBlock);
        block.SetCoreBlock(coreBlock);
    }

    private void RemoveBlockEntityAt(int location)
    {
        foreach (var child in GetChildren())
        {
            if (child is Block block)
            {
                var coreBlock = block.GetCoreBlock();
                if (coreBlock.Location == location)
                {
                    RemoveChild(block);
                    block.QueueFree();
                    world_.Entities.Remove(coreBlock);
                }
            }
        }
    }

}