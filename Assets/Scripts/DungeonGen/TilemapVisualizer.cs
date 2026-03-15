using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] Tilemap floorTileMap, wallTileMap;

    [SerializeField] private TileBase floorTile, wallTopTile;


    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos) 
    {
        PaintTiles(floorPos,floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> Pos, Tilemap TileMap, TileBase Tile)
    {
        foreach (var position in Pos)
        {
            PaintSingleTile(TileMap,Tile,position);
        }
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        var tilePos = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePos, tile);
    }

    public void Clear() 
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int pos)
    {
        PaintSingleTile(wallTileMap, wallTopTile,pos);
    }
}
