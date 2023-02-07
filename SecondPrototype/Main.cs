using Godot;
using SecondPrototype.nodes;

namespace SecondPrototype;

public partial class Main : Node2D
{
	public override void _Ready()
	{
		Position = Vector2.Zero;
		var screen = ScreenNode.Instantiate(this);
		var highlighter = HighlighterNode.Instantiate(this);
	}
}