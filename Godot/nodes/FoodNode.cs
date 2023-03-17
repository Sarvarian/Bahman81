using Godot;

namespace Survival.nodes;

public partial class FoodNode : EntityNode<aban.entities.Food>
{
	private static readonly StringName ScenePath = "res://scenes/food.tscn";
	private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

	public static FoodNode Instantiate(Node parent,
		Vector2I position,
		aban.entities.Food food,
		aban.Grid2D grid
	)
	{
		var node = Scene.Instantiate<FoodNode>();
		node.PrepareEntity(position, food, grid);
		parent.AddChild(node);
		return node;
	}

	protected override void OnLocationChanged()
	{
	}

	protected override void OnWorldOffsetUpdated()
	{
	}
}