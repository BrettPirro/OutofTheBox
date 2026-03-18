using System;
using System.Collections.Generic;
using UnityEngine;

public class BinaryRoomGen : DungeonGenSimple
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;

    [SerializeField]
    int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0,10)]
    int offset = 1;
    [SerializeField]
    bool randomWalkRooms = false;


    protected override void RunProceduralGen()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomList = ProceduralGenAlgo.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth,minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        floor = CreateSimpleRoom(roomList);

        tileMapVis.PaintFloorTiles(floor);
        WallGen.CreateWalls(floor, tileMapVis);
    }

    private HashSet<Vector2Int> CreateSimpleRoom(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var room in roomList)
        {
            for (int col = offset; col < room.size.x-offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int pos = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(pos);
                }


            }
        }
        return floor;
    }
}
