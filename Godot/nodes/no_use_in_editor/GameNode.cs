using Godot;
using Survival.aban;
using Survival.extensions;

namespace Survival.nodes.no_use_in_editor;

public partial class GameNode : CanvasLayer
{
    public static GameNode Instantiate(Node parent, InputHandler input)
    {
        var node = new GameNode(input);
        parent.AddChild(node);
        return node;
    }

    public override void _Ready()
    {
        base._Ready();
        GetViewport().SizeChanged += UpdateScreenSize;
        UpdateScreenSize();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    private GameNode(InputHandler input)
    {
        screen_ = new(Vector2I.Zero, Vector2I.Zero);
        world_ = new(Vector2I.Zero);
        scalar_ = new();
        grid_ = new Grid2D(new Vector2I(HardCoded.TileWidth, HardCoded.TileHeight));
        input_ = input;
        tiles_ = TilesNode.Instantiate(this, screen_, world_);
        SpawnCharacter();
    }

    private readonly GameScreen screen_;
    private readonly GameWorld world_;
    private readonly TheScalar scalar_;
    private readonly Grid2D grid_;
    private readonly InputHandler input_;
    private readonly TilesNode tiles_;

    private void UpdateScreenSize()
    {
        screen_.SetNewSize(GetViewport().GetVisibleRect().Size.ToVec2I());
    }

    private void SpawnCharacter()
    {

    }

}
