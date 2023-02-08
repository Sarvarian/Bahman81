using Godot;
using SecondPrototype.extensions;
using System.Collections.Generic;

namespace SecondPrototype.nodes;

public partial class RulerNumberLabelNode : Control
{
    private static readonly StringName ScenePath = "res://scenes/number.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static RulerNumberLabelNode Instantiate(Node parent)
    {
        var node = Scene.Instantiate<RulerNumberLabelNode>();
        return node;
    }

    [Export] private Label? label_;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(label_));
    }

    public void NewLocation(int location, aban.Grid2D grid)
    {
        label_!.Text = location.ToString();
        Position = grid.LocationToPosition(location);
        Name = GetName(location);
    }

    private static readonly Dictionary<int, StringName> Names = new();

    private static StringName GetName(int location)
    {
        if (Names.TryGetValue(location, out var oldName))
        {
            return oldName;
        }
        else
        {
            var newName = new StringName($"Number{location}");
            Names.Add(location, newName);
            return newName;
        }
    }

}