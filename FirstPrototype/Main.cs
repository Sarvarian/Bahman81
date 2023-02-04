using FirstPrototype.entities;
using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype;

public partial class Main : Node2D
{
    [Export] private PackedScene characterScene_;
    
    public override void _Ready()
    {
        this.AssertFiledSet(nameof(characterScene_));
        
        InstantiatePlayer();
    }

    private readonly CoreGame.World world_ = new();
    private Character player_;

    private void InstantiatePlayer()
    {
        player_ = characterScene_.Instantiate<Character>();
        AddChild(player_);
        player_.Position = new Vector2(50, 50);
    }
    
}