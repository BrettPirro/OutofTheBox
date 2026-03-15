using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenSimple : AbstractDungeonGen
{


    [SerializeField] public SimpleRandomWalkSO randomWalkParmameters;




    protected override void RunProceduralGen() 
    {
        HashSet<Vector2Int> floorPos = RunRandWalk();
        tileMapVis.Clear();
        tileMapVis.PaintFloorTiles(floorPos);
    }

    protected HashSet<Vector2Int> RunRandWalk()
    {
        var currentPos = startPos;
        HashSet<Vector2Int> floorpos = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkParmameters.iterations; i++)
        {
            var path = ProceduralGenAlgo.SimpleRandomWalk(currentPos, randomWalkParmameters.walkLength);
            floorpos.UnionWith(path);
            if (randomWalkParmameters.startRandomEachIteration)
                currentPos = floorpos.ElementAt(Random.Range(0, floorpos.Count));
        }
        return floorpos;
    }


}
