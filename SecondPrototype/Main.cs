using Godot;
using SecondPrototype.nodes;
using SecondPrototype.nodes.no_use_in_editor;
using MasterNodeOfRulerNumberLabelNodesAlsoANode = SecondPrototype.nodes.no_use_in_editor.MasterNodeOfRulerNumberLabelNodesAlsoANode;

namespace SecondPrototype;

public partial class Main : Node2D
{
    public override void _Ready()
    {
        Position = Vector2.Zero;
        var inputHandler = InputHandlerNode.Instantiate(this);
        var gridHandler = GridHandlerNode.Instantiate(this);
        var cameraHandler = CameraHandlerNode.Instantiate(this, gridHandler.Grid, inputHandler);
        var numberMaster = MasterNodeOfRulerNumberLabelNodesAlsoANode.Instantiate(
            this,
            gridHandler.Grid,
            cameraHandler.Camera
        );
        var debugDraw = DebugDrawNode.Instantiate(this, gridHandler.Grid, cameraHandler.Camera);
        player_ = CharacterNode.Instantiate(
            this,
            Vector2I.Zero,
            scalar_,
            gridHandler.Grid
        );
        player_.ConnectSignals(inputHandler);
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