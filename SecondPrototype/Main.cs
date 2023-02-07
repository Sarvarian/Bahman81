using Godot;
using SecondPrototype.nodes;

namespace SecondPrototype;

public partial class Main : Node2D
{
	public override void _Ready()
	{
		Position = Vector2.Zero;
		var gridHandler = GridHandlerNode.Instantiate(this);
		var cameraHandler = CameraHandlerNode.Instantiate(this, gridHandler.Grid);
		var player = CharacterNode.Instantiate(
			this,
			scalar_,
			gridHandler.Grid,
			Vector2I.Zero
		);
	}

	private readonly aban.TheScalar scalar_ = new();

}