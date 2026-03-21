using System;
using System.Collections.Generic;
using UnityEngine;

public static class WallGen 
{

    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPosition = FindWallsinDirection(floorPositions, direction2D.cardnialDir);
        var cornerWallPos = FindWallsinDirection(floorPositions, direction2D.diagonalDir);
        CreateBasicWalls(tilemapVisualizer, basicWallPosition,floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPos, floorPositions);
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPositions)
    {
        foreach (var pos in cornerWallPos)
        {
            string neighborsBinary = "";
            foreach (var dir in direction2D.eightdirlist)
            {
                var neighborPos = pos + dir;
                if (floorPositions.Contains(neighborPos)) { neighborsBinary += "1"; }
                else { neighborsBinary += "0"; }
            }
            tilemapVisualizer.PaintSingleCornerWall(pos, neighborsBinary);
        }
    }

    private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPosition, HashSet<Vector2Int> floorPositions)
    {
        foreach (var pos in basicWallPosition)
        {
            string neighborsBinary = "";

            foreach (var dir in direction2D.cardnialDir)
            {
                var neighborPos = pos + dir;
                if (floorPositions.Contains(neighborPos)) { neighborsBinary += "1"; }
                else { neighborsBinary += "0"; }
            }
            

                tilemapVisualizer.PaintSingleBasicWall(pos,neighborsBinary);
        }
    }

    private static HashSet<Vector2Int> FindWallsinDirection(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();

        foreach(var position in floorPositions) 
        {
            foreach (var dir in directionList)
            {
                var neighborPos = position + dir;
                if (floorPositions.Contains(neighborPos) == false) { wallPos.Add(neighborPos); }
            }
        }

        return wallPos;
    }
}
