using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype.entities;

public partial class Block : Node2D
{
    [Export] private Sprite2D? sprite2D_;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(sprite2D_));
    }

    public void SetCoreBlock(CoreGame.Block block)
    {
        coreBlock_ = block;
    }

    public CoreGame.Block GetCoreBlock()
    {
        return coreBlock_!;
    }

    private CoreGame.Block? coreBlock_;
}