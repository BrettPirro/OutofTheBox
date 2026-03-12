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
