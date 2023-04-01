using UnityEngine;

public static class HardCoded
{
	// Hard coded cause there are multiple place
	// in the project that follow theses numbers
	// but are separated and need to somehow
	// coordinate with each other.
	// 1) Designer Highlighter.
	// 2) Texture and sprite atlas.
	// 3) Tileset and TileMap.
	private const int TileWidth = 70;
	private const int TileHeight = 122;
	private const float PixelPerUnit = 100;
	private const float WorldOriginX = 0.0f;
	private const float WorldOriginY = 0.0f;

	public static Vector2 WorldOrigin
	{
		get
		{
			return new Vector2()
			{
				x = WorldOriginX,
				y = WorldOriginY
			};
		}
	}
	
	public static Vector2 CellSize
	{
		get
		{
			return new Vector2(TileWidth, TileHeight)
			{
				x = TileWidth / PixelPerUnit,
				y = TileHeight / PixelPerUnit
			};
		}
	} 
	
}
