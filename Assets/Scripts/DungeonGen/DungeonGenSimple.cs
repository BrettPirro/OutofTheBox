using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenSimple : AbstractDungeonGen
{


    [SerializeField] protected SimpleRandomWalkSO randomWalkParmameters;




    protected override void RunProceduralGen() 
    {
        HashSet<Vector2Int> floorPos = RunRandWalk(randomWalkParmameters,startPos);
        tileMapVis.Clear();
        tileMapVis.PaintFloorTiles(floorPos);
        WallGen.CreateWalls(floorPos, tileMapVis);
    }

    protected HashSet<Vector2Int> RunRandWalk(SimpleRandomWalkSO simpleRandomWalkSO, Vector2Int pos)
    {
        var currentPos = pos;
        HashSet<Vector2Int> floorpos = new HashSet<Vector2Int>();
        for (int i = 0; i < simpleRandomWalkSO.iterations; i++)
        {
            var path = ProceduralGenAlgo.SimpleRandomWalk(currentPos, simpleRandomWalkSO.walkLength);
            floorpos.UnionWith(path);
            if (simpleRandomWalkSO.startRandomEachIteration)
                currentPos = floorpos.ElementAt(Random.Range(0, floorpos.Count));
        }
        return floorpos;
    }


}
