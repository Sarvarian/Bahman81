using Godot;
using System.Collections.Generic;

namespace SecondPrototype.nodes.just_hide_them;

public partial class MasterNodeOfRulerNumberLabelNodesAlsoANode : Control
{
    public static MasterNodeOfRulerNumberLabelNodesAlsoANode Instantiate(
        Node parent,
        aban.Grid2D grid,
        CameraNode camera
    )
    {
        var node = new MasterNodeOfRulerNumberLabelNodesAlsoANode(grid, camera);
        parent.AddChild(node);
        return node;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        labels_.ForEach(label => label.QueueFree());
    }

    public override void _EnterTree()
    {
        base._EnterTree();
        ResetLabels();
    }

    private readonly aban.Grid2D grid_;
    private readonly CameraNode camera_;
    private readonly List<RulerNumberLabelNode> labels_ = new();

    private MasterNodeOfRulerNumberLabelNodesAlsoANode(aban.Grid2D grid, CameraNode camera)
    {
        grid_ = grid;
        camera_ = camera;
        ConnectSignals(camera);
    }

    private void ConnectSignals(CameraNode camera)
    {
        camera.PositionChangedSignal -= ResetLabels;
        camera.PositionChangedSignal += ResetLabels;
        camera.ZoomChangedSignal -= ResetLabels;
        camera.ZoomChangedSignal += ResetLabels;
        // We remove and then add signals just to prevent duplication.
    }

    private void ResetLabels()
    {
        labels_.ForEach(label =>
        {
            if (label.IsInsideTree())
            {
                RemoveChild(label);
            }
        });

        var screenSize = GetViewportRect().Size;
        var cameraZoom = camera_.TargetZoom;
        var area = grid_.HowManyFitsInScreenConsideringCameraZoom(screenSize, cameraZoom);
        var maxStep = (area.X / 2) + 2;

        var cameraPosition = camera_.GlobalPosition;
        var locationOffset = Mathf.FloorToInt(cameraPosition.X / grid_.CellSize.X);

        PutLabel(0 + locationOffset);
        for (var i = 1; i < maxStep; i++)
        {
            PutLabel((i * 1) + locationOffset);
            PutLabel((i * -1) + locationOffset);
        }
    }

    private void PutLabel(int location)
    {
        var label = labels_.Find(label => label.IsInsideTree() == false);
        if (label == null)
        {
            label = RulerNumberLabelNode.Instantiate(this);
            labels_.Add(label);
        }
        label.NewLocation(location, grid_);
        AddChild(label);
    }
}