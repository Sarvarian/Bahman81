using Godot;
using Survival.extensions;
using System;

namespace Survival.aban;

public class Grid2D
{
	public GameWorld? World;
	public Vector2I CellSize { get; private set; }
	public Action? CellSizeUpdatedSignal;

	public Grid2D(Vector2I cellSize)
	{
		CellSize = cellSize;
	}

	public void NewCellSize(Vector2I newSize)
	{
		if (CellSize != newSize)
		{
			CellSize = newSize;
			CellSizeUpdatedSignal?.Invoke();
		}
	}

	public Vector2 LocationToPosition(int location)
	{
		var offset = (Vector2)CalculateOffset();
		return new Vector2(location * CellSize.X, 0) + offset;
	}

	public Vector2 LocationToPosition(Vector2I location)
	{
		var offset = (Vector2)CalculateOffset();
		return (Vector2)(location * CellSize) + offset;
	}

	public Vector2I PositionToLocation(Vector2 position)
	{
		var offset = (Vector2)CalculateOffset();
		return ((position - offset) / (Vector2)CellSize).Round().ToVec2I();
	}

	public Vector2I HowManyFitsInScreen(Vector2 screenSize)
	{
		return (screenSize / CellSize).Floor().ToVec2I();
	}

	public Vector2I HowManyFitsInScreenConsideringCameraZoom(Vector2 screenSize, Vector2 cameraZoom)
	{
		return ((screenSize / cameraZoom) / CellSize).Floor().ToVec2I();
	}

	private Vector2I CalculateOffset()
	{
		return World?.Offset ?? Vector2I.Zero;
	}
}