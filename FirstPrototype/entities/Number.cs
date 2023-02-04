using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype.entities;

public partial class Number : Control
{
    [Export] private Label? label_;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(label_));
    }

    public void SetText(string text)
    {
        label_!.Text = text;
    }

}