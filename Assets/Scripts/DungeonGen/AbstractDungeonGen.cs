using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractDungeonGen : MonoBehaviour
{
    [SerializeField] protected TilemapVisualizer tileMapVis = null;

    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;

    public void GenDungeon() 
    {
        tileMapVis.Clear();
        RunProceduralGen();
    }

    protected abstract void RunProceduralGen();

}
