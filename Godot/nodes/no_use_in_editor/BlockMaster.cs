using Godot;

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
		handler.ImplantEntitySignal -= Implant;
		handler.ImplantEntitySignal += Implant;
		handler.RemoveEntitySignal -= Remove;
		handler.RemoveEntitySignal += Remove;
	}

	private void Implant()
	{
		if (highlighter_.IsVisibleInTree())
		{
			var location = grid_.PositionToLocation(highlighter_.Position).X;
			if (scalar_.EntitiesAt(location).Length == 0 && location != 0)
			{
				var node = BlockNode.Instantiate(
					this,
					scalar_,
					grid_,
					location
				);
			}
		}
	}

	private void Remove()
	{
		if (highlighter_.IsVisibleInTree())
		{
			foreach (var child in GetChildren())
			{
				if (child is BlockNode block && block.Position == highlighter_.Position)
				{
					block.Remove();
					break;
				}
			}
		}
	}


}