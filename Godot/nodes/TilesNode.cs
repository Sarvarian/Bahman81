using Godot;
using Survival.aban;
using Survival.extensions;

namespace Survival.nodes;

public partial class TilesNode : TileMap
{
    private static readonly Vector2I GroundTile = new(0, 9);
    private static readonly StringName ScenePath = "res://scenes/tiles.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static TilesNode Instantiate(Node parent, GameScreen screen, GameWorld world)
    {
        var node = Scene.Instantiate<TilesNode>();
        parent.AddChild(node);
        node.Initiate(screen, world);
        return node;
    }

    private GameScreen screen_ = new(Vector2I.Zero, Vector2I.Zero);
    private GameWorld world_ = new(Vector2I.Zero);
    private Vector2I origin_ = Vector2I.Zero; // World offset - TilesNode Position Offset.
    private Vector2I start_ = Vector2I.Zero; // Screen start - TilesNode Position Offset.
    private Vector2I end_ = Vector2I.Zero; // Screen end - TilesNode Position Offset.

    private void Initiate(GameScreen screen, GameWorld world)
    {
        screen_ = screen;
        world_ = world;
        screen_.SizeUpdatedSignal += Update;
        screen_.PositionUpdatedSignal += Update;
        world_.OffsetUpdatedSignal += Update;
        Update();
    }

    private void Update()
    {
        Clear();
        var positionOffset = world_.Offset % TileSet.TileSize;
        positionOffset.X -= TileSet.TileSize.X / 2;
        Position = positionOffset;
        origin_ = world_.Offset - Position.ToVec2I();
        start_ = screen_.Start - Position.ToVec2I();
        end_ = screen_.End - Position.ToVec2I();
        TileGround();
    }

    private void Tile(Vector2I coords, Vector2I tile)
    {
        SetCell(0, coords, 0, tile);
    }


    private void TileGround()
    {
        Tile(LocalToMap(origin_), GroundTile);

        var originTile = LocalToMap(origin_);
        var startTile = LocalToMap(start_);
        var endTile = LocalToMap(end_);

        var i = startTile.X;
        int j; // Either StartTile.Y or originTile.Y .
        CalculateJ();

        for (; i <= endTile.X; i++)
        {
            for (; j <= endTile.Y; j++)
            {
                Tile(new Vector2I(i, j), GroundTile);
            }
            CalculateJ();
        }

        void CalculateJ() => j = Mathf.Max(startTile.Y, originTile.Y);
    }

}
