using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualiser : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap;

    [SerializeField] private TileBase floorTile, wallTop, wallRight, wallLeft, wallBottom, wallFull,
    wallInnerCornerDownLeft, wallInnerCornerDownRight,
    wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft,
    ExitClosed, ExitOpen;

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position){
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile){
        foreach (var position in positions){
            PaintSingleTile(tilemap, tile, position);
        }
    }
    public void PaintExitTile(Vector2Int position){
        PaintSingleTile(floorTilemap, ExitClosed, position);
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions){
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    public void PaintSingleBasicWall(Vector2Int position, string binaryType){
        int typeAsInt = Convert.ToInt16(binaryType, 2);
        TileBase tile = null;
        
        if(WallTypesHelper.wallTop.Contains(typeAsInt)) tile = wallTop;
        else if(WallTypesHelper.wallSideLeft.Contains(typeAsInt)) tile = wallLeft;
        else if(WallTypesHelper.wallSideRight.Contains(typeAsInt)) tile = wallRight;
        else if(WallTypesHelper.wallBottm.Contains(typeAsInt)) tile = wallBottom;
        else if(WallTypesHelper.wallFull.Contains(typeAsInt)) tile = wallFull;

        if(tile != null) PaintSingleTile(wallTilemap, tile, position);
        else PaintSingleTile(wallTilemap, wallTop, position);
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt16(binaryType, 2);
        TileBase tile = null;

        if(WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt)) tile = wallDiagonalCornerDownLeft;
        else if(WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt)) tile = wallDiagonalCornerDownRight;
        else if(WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt)) tile = wallDiagonalCornerUpRight;
        else if(WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt)) tile = wallDiagonalCornerUpLeft;
        else if(WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt)) tile = wallInnerCornerDownRight;
        else if(WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt)) tile = wallInnerCornerDownLeft;
        else if(WallTypesHelper.wallFullEightDirections.Contains(typeAsInt)) tile = wallFull;
        else if(WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt)) tile = wallBottom;

        if(tile != null) PaintSingleTile(wallTilemap, tile, position);
    }

    public void Clear(){
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
    public List<TileBase> GetExitTiles()
    {
        return new List<TileBase>{ExitClosed, ExitOpen};
    }
    public void OpenExit()
    {
        floorTilemap.SwapTile(ExitClosed, ExitOpen);
    }
}
