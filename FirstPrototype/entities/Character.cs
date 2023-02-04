using System.Reflection;
using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype.entities;

public partial class Character : Node2D
{
    [Export] private Sprite2D sprite2D_;
    
    public override void _Ready()
    {
        this.AssertFiledSet(nameof(sprite2D_));
    }

    public override void _Process(double delta)
    {
    }
}