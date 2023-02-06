using Godot;
using SecondPrototype.nodes;

namespace SecondPrototype;

public partial class Main : Node2D
{
	public override void _Ready()
	{
		Position = Vector2.Zero;
		ScreenAgent.Setup(this);
	}
}