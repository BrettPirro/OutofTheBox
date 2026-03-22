using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField] DungeonData dungeonData;


    private void Awake()
    {
        tileMapVis.Clear();
        RunProceduralGen();
        this.GetComponent<RoomDataExtractor>().ProcessRooms();

    }


    protected override void RunProceduralGen()
    {
 

        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomList = ProceduralGenAlgo.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth,minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomList);
        }
        else 
        {
            floor = CreateSimpleRoom(roomList);

        }



        List<Vector2Int> roomCenters = new List<Vector2Int>();

        foreach (var room in roomList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);

      



        floor.UnionWith(corridors);
        tileMapVis.PaintFloorTiles(floor);
        WallGen.CreateWalls(floor, tileMapVis);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomList.Count; i++)
        {
            var roomBounds = roomList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandWalk(randomWalkParmameters, roomCenter);
            dungeonData.Rooms.Add(new Room(roomCenter, roomFloor));
            foreach (var pos in roomFloor)
            {
                if(pos.x>=(roomBounds.xMin+offset)&& pos.x <= (roomBounds.xMax - offset) && pos.y >= (roomBounds.yMin - offset)&& pos.y <= (roomBounds.yMax - offset)) 
                {
                    floor.Add(pos);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count>0)
        {
            Vector2Int closet = FindClosetPointTo(currentRoomCenter,roomCenters);
            roomCenters.Remove(closet);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closet);
            currentRoomCenter = closet;
            corridors.UnionWith(newCorridor);
        }

        dungeonData.Path = corridors;
        return corridors;

    }



    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int closet)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var pos = currentRoomCenter;



        corridor.Add(pos);

        while (pos.y != closet.y)
        {
            Vector2Int direction;

            if (closet.y > pos.y)
                direction = Vector2Int.up;
            else
                direction = Vector2Int.down;

            pos += direction;

            corridor.Add(pos);


            Vector2Int perp = new Vector2Int(-direction.y, direction.x);

            corridor.Add(pos + perp);      
            corridor.Add(pos - perp);
        }

        while (pos.x != closet.x)
        {
            Vector2Int direction;

            if (closet.x > pos.x)
                direction = Vector2Int.right;
            else
                direction = Vector2Int.left;

            pos += direction;

            corridor.Add(pos);

            Vector2Int perp = new Vector2Int(-direction.y, direction.x);

            corridor.Add(pos + perp);     
            corridor.Add(pos - perp);
        }
        return corridor;
    }




    private Vector2Int FindClosetPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closet = Vector2Int.zero;
        float length = float.MaxValue;

        foreach (var pos in roomCenters)
        {
            float currentDist = Vector2.Distance(pos, currentRoomCenter);

            if (currentDist<length) 
            { 
                length = currentDist;
                closet = pos;
            }
        }
        return closet;

    }

    private HashSet<Vector2Int> CreateSimpleRoom(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var room in roomList)
        {
            HashSet<Vector2Int> floorCurrent = new HashSet<Vector2Int>();

            for (int col = offset; col < room.size.x-offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int pos = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(pos);
                    floorCurrent.Add(pos);
                }


            }
            dungeonData.Rooms.Add(new Room(room.center, floorCurrent));


        }
        return floor;
    }
}
