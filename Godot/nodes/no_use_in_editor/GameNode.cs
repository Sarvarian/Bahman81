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
        InitiateWorldOffset();
        SpawnPlayerCharacter();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        DoTheTick();
    }

    private GameNode(InputHandler input)
    {
        screen_ = new(Vector2I.Zero, Vector2I.Zero);
        world_ = new(Vector2I.Zero);
        scalar_ = new();
        grid_ = new Grid2D(new Vector2I(HardCoded.TileWidth, HardCoded.TileHeight))
        {
            World = world_
        };
        input_ = input;
        tiles_ = TilesNode.Instantiate(this, screen_, world_);
    }

    private readonly GameScreen screen_;
    private readonly GameWorld world_;
    private readonly TheScalar scalar_;
    private readonly Grid2D grid_;
    private readonly InputHandler input_;
    private readonly TilesNode tiles_;
    private CharacterNode? player_;

    private void UpdateScreenSize()
    {
        screen_.SetNewSize(GetViewport().GetVisibleRect().Size.ToVec2I());
    }

    private void InitiateWorldOffset()
    {
        var newOffset = screen_.Center;
        newOffset.Y = grid_.CellSize.Y * 5;
        // 1 for characters and playground and
        // 4 for showing pulley and rope connection between gates and switches.
        world_.SetNewOffset(newOffset);
    }

    private void SpawnPlayerCharacter()
    {
        player_ = CharacterNode.Instantiate(this, world_.Offset, scalar_, grid_);
        player_.ConnectSignals(input_);
    }

    private void DoTheTick()
    {
        if (player_!.IsGotInputEvent)
        {
            if (IsAnyEntityActive() == false)
            {
                scalar_.Tick();
                player_.IsGotInputEvent = false;
            }
        }
    }

    private bool IsAnyEntityActive()
    {
        foreach (var node in GetChildren())
        {
            if (node is IEntityNode entity)
            {
                if (entity.IsActive())
                {
                    return true;
                }
            }
        }
        return false;
    }

}

/*
 * 
class GameMaster
{

    public GameMaster(TheScalar scalar)
    {
        scalar.PostTick += Tick;
        InitiatePlay();
    }

    void InitiatePlay()
    {
        
    }

    void Tick()
    {
        
    }
    
}
 */
