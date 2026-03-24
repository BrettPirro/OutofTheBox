using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] Tilemap floorTileMap, wallTileMap;

    [SerializeField] private TileBase floorTile;

    [SerializeField] private List<GameTileDirSets> tiles;


    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos) 
    {
        PaintTiles(floorPos,floorTileMap, tiles[BoxLevelManager.current.levelNum].Floor );
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

    internal void PaintSingleBasicWall(Vector2Int pos,string binarytype)
    {
        int typeAsInt = Convert.ToInt32(binarytype, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallTopTile; }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallSideRightTile; }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallSideLeftTile; }
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallBottpmTile; }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallFull; }



        if (tile!=null)
        {
            PaintSingleTile(wallTileMap, tile, pos);
        }


    }

    internal void PaintSingleCornerWall(Vector2Int pos, string binarytype)
    {
        int typeAsInt = Convert.ToInt32(binarytype, 2);
        TileBase tile = null;


        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallinnercornerDownLeft; }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallinnercornerDownRight; }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallDiagonalCornerDownRight; }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallDiagonalCornerDownLeft; }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallDiagonalCornerUpRight; }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt)) { tile = tiles[BoxLevelManager.current.levelNum].wallDiagonalCornerUpLeft; }







        if (tile != null)
        {
            PaintSingleTile(wallTileMap, tile, pos);
        }


    }
}
