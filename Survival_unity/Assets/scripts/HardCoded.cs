using System.Collections;
using System.Collections.Generic;
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
	private const int PixelPerUnit = 100;

	public static Vector2Int CellSize
	{
		get
		{
			return new Vector2Int(TileWidth, TileHeight)
			{
				x = TileWidth / PixelPerUnit,
				y = TileHeight / PixelPerUnit
			};
		}
	} 
	
}
