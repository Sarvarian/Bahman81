using Godot;
using System;

namespace Survival.nodes.no_use_in_editor;

public partial class BlockMaster : Node2D
{
    public static BlockMaster Instantiate(
        Node parent,
        aban.TheScalar scalar,
        aban.Grid2D grid,
        HighlighterNode highlighter,
        InputHandler input
    )
    {
        var node = new BlockMaster(scalar, grid, highlighter, input);
        parent.AddChild(node);
        return node;
    }

    private readonly aban.TheScalar scalar_;
    private readonly aban.Grid2D grid_;
    private readonly HighlighterNode highlighter_;

    private BlockMaster(
        aban.TheScalar scalar,
        aban.Grid2D grid,
        HighlighterNode highlighterNode,
        InputHandler inputHandler
    )
    {
        Name = nameof(BlockMaster);
        scalar_ = scalar;
        grid_ = grid;
        highlighter_ = highlighterNode;
        ConnectSignals(inputHandler);
    }

    private void ConnectSignals(InputHandler handler)
    {
        handler.ImplantRemoveBlockSignal -= ImplantRemoveBlock;
        handler.ImplantRemoveBlockSignal += ImplantRemoveBlock;
        handler.ImplantRemoveSwitchSignal -= ImplantRemoveSwitch;
        handler.ImplantRemoveSwitchSignal += ImplantRemoveSwitch;
    }

    private void ImplantRemoveBlock()
    {
        ImplantRemove(
            location => BlockNode.Instantiate(
                this,
                scalar_,
                grid_,
                location
            )
        );
    }

    private void ImplantRemoveSwitch()
    {
        ImplantRemove(
            location => SwitchNode.Instantiate(
                this,
                scalar_,
                grid_,
                location
            )
        );
    }

    private void ImplantRemove<T>(Func<int, T> instantiate)
    {
        if (highlighter_.IsVisibleInTree())
        {
            var location = grid_.PositionToLocation(highlighter_.Position).X;
            if (location == 0)
            {
                return;
            }

            foreach (var child in GetChildren())
            {
                if (child is Node2D node && node.Position == highlighter_.Position)
                {
                    switch (node)
                    {
                        case BlockNode block:
                            block.Remove();
                            return;
                        case SwitchNode @switch:
                            @switch.Remove();
                            return;
                    }
                }
            }

            if (scalar_.EntitiesAt(location).Length == 0)
            {
                var unused = instantiate(location);
            }
        }
    }

}