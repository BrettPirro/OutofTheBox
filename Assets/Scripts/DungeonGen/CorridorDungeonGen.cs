using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorDungeonGen : DungeonGenSimple
{
    [SerializeField]
    private int corridorLength = 14;
    [SerializeField]
    private int corridorCount = 5;

    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent = .8f;

    protected override void RunProceduralGen()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> PotentialRooms = new HashSet<Vector2Int>();

        CreateCorridors(floorPos,PotentialRooms);
        HashSet<Vector2Int> roomPos = CreateRooms(PotentialRooms);

        floorPos.UnionWith(roomPos);

        tileMapVis.PaintFloorTiles(floorPos);
        WallGen.CreateWalls(floorPos, tileMapVis);
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRooms)
    {
        HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRooms.Count * roomPercent);

        List<Vector2Int> roomtoCreate = potentialRooms.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPositions in roomtoCreate)
        {
            var roomFloor = RunRandWalk(randomWalkParmameters, roomPositions);
            roomPos.UnionWith(roomFloor);
        }

        return roomPos;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> PotentialRooms)
    {
        var currentpos = startPos;
        PotentialRooms.Add(currentpos);
        for (int i = 0; i < corridorCount; i++)
        {
            var path = ProceduralGenAlgo.RandomWalkCorridor(currentpos, corridorLength);
            currentpos = path[path.Count - 1];
            PotentialRooms.Add(currentpos);
            floorPos.UnionWith(path);
        }
    }
}
