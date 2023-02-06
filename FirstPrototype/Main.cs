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
        ResetScreenSize();
        InstantiateHighlighter();
        CreateGroundRuler();
        InstantiatePlayer();
        InitializeDebugDrawGroundRuler();
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

    public override void _Draw()
    {
        base._Draw();
        debugDrawGroundRuler_!.Draw();
    }

    private readonly InputHandler inputHandler_ = new();
    private readonly CoreGame.World world_ = new();
    private readonly CoreGame.Screen screen_ = new(Vector2I.Zero.IntoCoreVector2I());
    private Character? player_;
    private Highlighter? highlighter_;
    private DebugDrawGroundRuler? debugDrawGroundRuler_;
    private int pixelPerGroundRulerStep_;

    private void InstantiatePlayer()
    {
        player_ = characterScene_!.Instantiate<Character>();
        AddChild(player_);
        player_.Position = screen_.Center.IntoGodotVector2I();
        player_.ConnectSignals(inputHandler_);
        player_.SetCoreCharacter(world_.Player);
    }

    private void ConnectSignals()
    {
        GetTree().Root.SizeChanged += ResetScreenSize;
        inputHandler_.ImplantEntitySignal += OnInputHandlerImplantEntitySignal;
        inputHandler_.RemoveEntitySignal += OnInputHandlerRemoveEntitySignal;
    }

    private void ResetScreenSize()
    {
        var oldSize = screen_.Size.IntoGodotVector2I();
        var newSize = GetTree().Root.GetVisibleRect().Size.IntoGodotVector2I();
        if (oldSize != newSize)
        {
            screen_.NewSize(newSize.IntoCoreVector2I());
        }
    }

    private void CreateGroundRuler()
    {
        InstantiateANumberLabel(0);
        var maxStep = screen_.Center.X / pixelPerGroundRulerStep_;
        for (var i = 1; i < maxStep; i++)
        {
            InstantiateANumberLabel(i * 1);
            InstantiateANumberLabel(i * -1);
        }
    }

    private void InstantiateANumberLabel(int location)
    {
        var num = numberLabelScene_!.Instantiate<Number>();
        num.Position = screen_.Center.IntoGodotVector2I();
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
        player_!.UpdateLocation(screen_.Center.IntoGodotVector2I(), pixelPerGroundRulerStep_);
    }

    private void InstantiateHighlighter()
    {
        highlighter_ = highlighterScene_!.Instantiate<Highlighter>();
        pixelPerGroundRulerStep_ = highlighter_.GetWidth();
        AddChild(highlighter_);
        highlighter_.Position = Vector2.Zero;
        highlighter_.SetScreen(screen_);
        screen_.SetPixelPerGroundRulerStep(pixelPerGroundRulerStep_);
    }

    private Vector2 WhereToPutHighlighter()
    {
        if (highlighter_!.IsMouseCloseAndAboveGround() == false)
        {
            return Vector2.Zero;
        }

        var res = Vector2.Zero;
        res.Y = screen_.Center.Y;

        var mouseX = Mathf.RoundToInt(GetGlobalMousePosition().X);
        var rulerPoint = screen_.LocationOfPointInGroundRuler(mouseX);
        res.X = screen_.Center.X + (rulerPoint * pixelPerGroundRulerStep_);

        return res;
    }

    private bool IsHighlighterInactive()
    {
        return highlighter_!.Position == Vector2.Zero;
    }

    private int HighlighterLocation()
    {
        return ((int)highlighter_!.Position.X - screen_.Center.X) / pixelPerGroundRulerStep_;
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
            Y = screen_.Center.Y,
            X = screen_.Center.X + (location * pixelPerGroundRulerStep_)
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

    private void InitializeDebugDrawGroundRuler()
    {
        debugDrawGroundRuler_ = new(this, screen_, pixelPerGroundRulerStep_);
    }

}