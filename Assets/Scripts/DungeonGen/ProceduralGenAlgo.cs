using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenAlgo 
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength) 
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPos);
        var prevPosition = startPos;
        

        for(int i =0; i < walkLength; i++) 
        {
            var newpos = prevPosition + direction2D.GetRandonDir();
            path.Add(newpos);
            prevPosition = newpos;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength) 
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = direction2D.GetRandonDir();
        var currentPos = startPos;
        corridor.Add(currentPos);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;
    
    }


    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight) 
    {
        Queue<BoundsInt> RoomQue = new Queue<BoundsInt>();

        List<BoundsInt> roomsList = new List<BoundsInt>();

        RoomQue.Enqueue(spaceToSplit);

        while (RoomQue.Count > 0) 
        {
            var room = RoomQue.Dequeue();
            if(room.size.y>= minHeight && room.size.x >= minWidth) 
            {
                if (Random.value < .5f) 
                {
                    if(room.size.y>= minHeight * 2) 
                    {
                        SplitHorizontally(minWidth,  RoomQue, room);
                    }
                    else if(room.size.x >= minWidth * 2) 
                    {
                        SplitVertically( minHeight, RoomQue, room);
                    }
                    else 
                    {
                        roomsList.Add(room);
                    }
                }
                else 
                {
              
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, RoomQue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally( minHeight, RoomQue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomQue, BoundsInt room)
    {
        var xsplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xsplit,room.size.y,room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xsplit, room.min.y, room.min.z), new Vector3Int(room.size.x-xsplit, room.size.y,room.size.z));

        roomQue.Enqueue(room1);
        roomQue.Enqueue(room2);


    }

    private static void SplitHorizontally( int minHeight, Queue<BoundsInt> roomQue, BoundsInt room)
    {
        var ysplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ysplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ysplit, room.min.z), new Vector3Int(room.size.x, room.size.y - ysplit, room.size.z));

        roomQue.Enqueue(room1);
        roomQue.Enqueue(room2);

    }
}


public static class direction2D 
{
    public static List<Vector2Int> cardnialDir= new List<Vector2Int> 
    {
        new Vector2Int(0,1) , //UP
        new Vector2Int(1,0) , //Right
        new Vector2Int(0,-1) , //Down
        new Vector2Int(-1,0) , //Left


        
    };

    public static List<Vector2Int> diagonalDir = new List<Vector2Int>
    {
        new Vector2Int(1,1) , //UP-RIGHT
        new Vector2Int(1,-1) , //Right DOWN
        new Vector2Int(-1,-1) , //Down LEFT
        new Vector2Int(-1,1) , //Left UP


        
    };

    public static List<Vector2Int> eightdirlist = new List<Vector2Int> 
    {
        new Vector2Int(0,1) , //UP
        new Vector2Int(1,1) , //UP-RIGHT
        new Vector2Int(1,0) , //Right
        new Vector2Int(1,-1) , //Right DOWN
        new Vector2Int(0,-1) , //Down
        new Vector2Int(-1,-1) , //Down LEFT
        new Vector2Int(-1,0) , //Left
        new Vector2Int(-1,1) , //Left UP

    
    };

    public static Vector2Int GetRandonDir() 
    {
        return cardnialDir[Random.Range(0, cardnialDir.Count)];
    }


}
