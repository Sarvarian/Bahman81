using Godot;
using Survival.aban;

namespace Survival.nodes;

public partial class TilesNode : TileMap
{
	private static readonly StringName ScenePath = "res://scenes/tiles.tscn";
	private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

	public static TilesNode Instantiate(Node parent, GameScreen screen, GameWorld world)
	{
		var node = Scene.Instantiate<TilesNode>();
		parent.AddChild(node);
		node.screen_ = screen;
		node.world_ = world;
		return node;
	}

	private GameScreen screen_ = new(Vector2I.Zero, Vector2I.Zero);
	private GameWorld world_ = new(Vector2I.Zero);


}
