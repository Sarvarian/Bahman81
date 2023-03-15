using Godot;
using Survival.nodes;
using Survival.nodes.no_use_in_editor;

namespace Survival;

public partial class MainDesigner : Node2D
{
	public override void _Ready()
	{
		Position = Vector2.Zero;
		var inputHandler = InputHandler.Instantiate(this);
		var gridHandler = GridHandler.Instantiate(this, scalar_, inputHandler);
		var cameraHandler = CameraHandler.Instantiate(this, gridHandler.Grid, inputHandler);
		var numberMaster = MasterNumber.Instantiate(
			this,
			gridHandler.Grid,
			cameraHandler.Camera
		);
		var debugDraw = DebugDraw.Instantiate(this, gridHandler.Grid, cameraHandler.Camera);
		player_ = CharacterNode.Instantiate(
			this,
			Vector2I.Zero,
			scalar_,
			gridHandler.Grid
		);
		player_.ConnectSignals(inputHandler);
		var blockMaster = BlockMaster.Instantiate(
			this,
			scalar_,
			gridHandler.Grid,
			gridHandler.Highlighter,
			inputHandler
		);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		DoTheTick();
	}

	private readonly aban.TheScalar scalar_ = new();
	private CharacterNode? player_;

	private void DoTheTick()
	{
		if (player_!.IsGotInputEvent)
		{
			scalar_.Tick();
			player_.IsGotInputEvent = false;
		}
	}

}
