using Godot;

namespace Survival.nodes.no_use_in_editor;

public partial class GridHandler : Node2D
{

	public static GridHandler Instantiate(Node parent, aban.TheScalar scalar, InputHandler input)
	{
		var node = new GridHandler(scalar, input);
		node.Name = nameof(GridHandler);
		parent.AddChild(node);
		return node;
	}

	public readonly aban.Grid2D Grid;
	public readonly HighlighterNode Highlighter;

	private readonly aban.TheScalar scalar_;

	private GridHandler(aban.TheScalar scalar, InputHandler inputHandler)
	{
		Highlighter = HighlighterNode.Instantiate(this);
		Grid = new(CalculateCellSize());
		scalar_ = scalar;
		Highlighter.Hide();
		ConnectSignals(inputHandler);
	}

	private Vector2I CalculateCellSize()
	{
		var width = Highlighter.GetSize().X;
		return new Vector2I(width, width);
	}

	private void ConnectSignals(InputHandler handler)
	{
		handler.MouseMovedSignal -= OnMouseMoved;
		handler.MouseMovedSignal += OnMouseMoved;
		// We remove and then add signals just to prevent duplication.
	}

	private void OnMouseMoved(InputEventMouseMotion mouse)
	{
		var position = GetGlobalMousePosition();
		var height = Highlighter.GetSize().Y;
		var topAreaCheck = position.Y > -height;
		var bottomAreaCheck = position.Y < height;
		if (topAreaCheck && bottomAreaCheck)
		{
			var location = Grid.PositionToLocation(position);
			Highlighter.Visible = true;
			Highlighter.GlobalPosition = Grid.LocationToPosition(location.X);
			if (scalar_.EntitiesAt(location.X).Length == 0 && location.X != 0)
			{
				Highlighter.GoFreeColor();
			}
			else
			{
				Highlighter.GoOccupiedColor();
			}
		}
		else
		{
			Highlighter.Hide();
		}
	}

}