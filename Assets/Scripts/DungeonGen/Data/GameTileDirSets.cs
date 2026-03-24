using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="GameSets")]
public class GameTileDirSets : ScriptableObject
{

    public TileBase wallTopTile, wallSideRightTile, wallSideLeftTile, wallBottpmTile, wallFull,
        wallinnercornerDownLeft, wallinnercornerDownRight,
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft,Floor;


}
