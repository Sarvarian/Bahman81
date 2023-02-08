using Godot;

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

    private MasterNodeOfRulerNumberLabelNodesAlsoANode(aban.Grid2D grid, CameraNode camera)
    {
        grid_ = grid;
        camera_ = camera;
        ResetLabels();
    }

    private readonly aban.Grid2D grid_;
    private readonly CameraNode camera_;

    private void ResetLabels()
    {
        // TODO: To be implemented!
    }

}