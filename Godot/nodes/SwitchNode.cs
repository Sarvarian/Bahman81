using Godot;
using Survival.extensions;

namespace Survival.nodes;

public partial class SwitchNode : Node2D
{
	private static readonly StringName ScenePath = "res://scenes/switch.tscn";
	private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

	public static SwitchNode Instantiate(
		Node parent,
		aban.TheScalar scalar,
		aban.Grid2D grid,
		int location
	)
	{
		var node = Scene.Instantiate<SwitchNode>();
		parent.AddChild(node);
		node.SetSwitch(scalar, grid, location);
		return node;
	}

	[Export] private Sprite2D? sprite2D_;

	public override void _Ready()
	{
		this.AssertFiledSet(nameof(sprite2D_));
	}

	public void Remove()
	{
		scalar_!.Entities.Remove(switch_!);
		QueueFree();
	}

	private void SetSwitch(aban.TheScalar scalar, aban.Grid2D grid, int location)
	{
		switch_ = new aban.entities.Switch(location);
		Position = grid.LocationToPosition(location);
		scalar.Entities.Add(switch_);
		scalar_ = scalar;
	}

	private aban.entities.Switch? switch_;
	private aban.TheScalar? scalar_;

}