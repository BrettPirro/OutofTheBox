using System;
using System.Collections.Generic;
using UnityEngine;

public static class WallGen 
{

    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer) 
    {
        var basicWallPosition = FindWallsinDirection(floorPositions, direction2D.cardnialDir);
        foreach(var pos in basicWallPosition) 
        {
            tilemapVisualizer.PaintSingleBasicWall(pos);
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
