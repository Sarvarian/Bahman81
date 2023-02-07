using Godot;
using SecondPrototype.nodes;

namespace SecondPrototype;

public partial class Main : Node2D
{
	public override void _Ready()
	{
		Position = Vector2.Zero;
		var inputHandler = InputHandler.Instantiate(this);
		var gridHandler = GridHandlerNode.Instantiate(this);
		var cameraHandler = CameraHandlerNode.Instantiate(this, gridHandler.Grid);
		var player = CharacterNode.Instantiate(
			this,
			Vector2I.Zero,
			scalar_,
			gridHandler.Grid
		);
	}

	private readonly aban.TheScalar scalar_ = new();

}