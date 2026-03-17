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
                        SplitHorizontally(minWidth, minHeight, RoomQue, room);
                    }
                    else if(room.size.x >= minWidth * 2) 
                    {
                        SplitVertically(minWidth, minHeight, RoomQue, room);
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
                        SplitVertically(minWidth, minHeight, RoomQue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, minHeight, RoomQue, room);
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

    private static void SplitVertically(int minWidth, int minHeight, Queue<BoundsInt> roomQue, BoundsInt room)
    {
        throw new System.NotImplementedException();
    }

    private static void SplitHorizontally(int minWidth, int minHeight, Queue<BoundsInt> roomQue, BoundsInt room)
    {
        throw new System.NotImplementedException();
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

    public static Vector2Int GetRandonDir() 
    {
        return cardnialDir[Random.Range(0, cardnialDir.Count)];
    }


}
