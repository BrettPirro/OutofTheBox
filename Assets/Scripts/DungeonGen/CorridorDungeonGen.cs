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

        List<List<Vector2Int>> corridors= CreateCorridors(floorPos,PotentialRooms);
        HashSet<Vector2Int> roomPos = CreateRooms(PotentialRooms);

        List<Vector2Int> deadEnds = findAllDeadEnds(floorPos);

        createRoomsAtDeadEnd(deadEnds,roomPos);


        floorPos.UnionWith(roomPos);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorsSizeByOne(corridors[i]);
            floorPos.UnionWith(corridors[i]);

        }


        tileMapVis.PaintFloorTiles(floorPos);
        WallGen.CreateWalls(floorPos, tileMapVis);
    }

    private List<Vector2Int> IncreaseCorridorsSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCor = new List<Vector2Int>();
        Vector2Int previewDir = Vector2Int.zero;

        for (int i = 1; i < corridor.Count; i++)
        {
            Vector2Int dirCell = corridor[i] - corridor[i - 1];
            if(previewDir != Vector2Int.zero && dirCell != previewDir) 
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }
                previewDir = dirCell;
            
            }
            else 
            {
                Vector2Int newCorTileOffset = getDir90From(dirCell);
                newCor.Add(corridor[i - 1]);
                newCor.Add(corridor[i - 1]+ newCorTileOffset);
                previewDir = dirCell;
            }



        }
        return newCor;
    }

    private Vector2Int getDir90From(Vector2Int dirCell)
    {
        if (dirCell == Vector2Int.up) { return Vector2Int.right; }
        else if(dirCell== Vector2Int.right) { return Vector2Int.down; }
        else if(dirCell== Vector2Int.down) { return Vector2Int.left; }
        else if(dirCell == Vector2Int.left) { return Vector2Int.up; }
        else { return Vector2Int.zero; }
    }

    private void createRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPos)
    {
        foreach (var pos in deadEnds)
        {
            if (roomPos.Contains(pos) == false) 
            {
                var roomFloor = RunRandWalk(randomWalkParmameters, pos);
                roomPos.UnionWith(roomFloor);
            }
        }
    
    }

    private List<Vector2Int> findAllDeadEnds(HashSet<Vector2Int> floorPos)
    {

        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var pos in floorPos)
        {
            int neighborsCount = 0;
            foreach (var dir in direction2D.cardnialDir)
            {
                if (floorPos.Contains(pos + dir))
                    neighborsCount++;
                
            }
            if (neighborsCount == 1)
                deadEnds.Add(pos);
        }
        return deadEnds;
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

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> PotentialRooms)
    {
        var currentpos = startPos;
        PotentialRooms.Add(currentpos);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var path = ProceduralGenAlgo.RandomWalkCorridor(currentpos, corridorLength);
            corridors.Add(path);
            currentpos = path[path.Count - 1];
            PotentialRooms.Add(currentpos);
            floorPos.UnionWith(path);
        }

        return corridors;
    }
}
