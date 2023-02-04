using FirstPrototype.entities;
using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype;

public partial class Main : Node2D
{
    [Export] private PackedScene characterScene_;

    public override void _Ready()
    {
        Position = Vector2.Zero;

        this.AssertFiledSet(nameof(characterScene_));

        ConnectSignals();
        InitializeScreen();
        InstantiatePlayer();
    }

    private readonly CoreGame.World world_ = new();
    private readonly CoreGame.Screen screen_ = new(0, 0);
    private Character player_;

    private void InstantiatePlayer()
    {
        player_ = characterScene_.Instantiate<Character>();
        AddChild(player_);
        player_.Position = new Vector2(50, 50);
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
        var newSize = GetTree().Root.GetVisibleRect().Size;
        screen_.NewSize((int)newSize.X, (int)newSize.Y);
        GD.Print($"W: {screen_.Width}, H: {screen_.Height}");
    }
}