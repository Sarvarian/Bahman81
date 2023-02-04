using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype.entities;

public partial class Number : Control
{
    [Export] private Label label_;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(label_));
    }

}