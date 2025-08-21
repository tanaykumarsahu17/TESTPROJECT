using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour //Stores Grid coordinate for a tile
{
    public int x, y; //grid coordinates of this tile 

    public void SetCoordinates(int xPos, int yPos) //assigns grid coordinates to the tile
    {
        x = xPos;
        y = yPos;
    }

    public string GetInfo()
    {
        return $"Tile Position: ({x}, {y})"; //returns formatted string with tile coordinates 
    }

    public Vector2Int coordinates => new Vector2Int(x, y);
}