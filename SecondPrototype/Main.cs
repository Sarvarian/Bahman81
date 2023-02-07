using Godot;
using SecondPrototype.nodes;

namespace SecondPrototype;

public partial class Main : Node2D
{
	public override void _Ready()
	{
		Position = Vector2.Zero;
		var screen = ScreenAgent.Instantiate(this);
		var highlighter = HighlighterNode.Instantiate(this);
		var player = CharacterNode.Instantiate(this, screen.Screen.Center);
	}
}