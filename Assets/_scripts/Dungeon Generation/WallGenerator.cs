using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    private static HashSet<Vector2Int> FindWallPositions(HashSet<Vector2Int> floorpositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach(var position in floorpositions){
            foreach(var direction in directionList){
                var neighborPosition = position + direction;
                if(floorpositions.Contains(neighborPosition) == false){
                    wallPositions.Add(neighborPosition);
                }
            }
        }
        return wallPositions;
    }
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualiser tilemapVisualiser)
    {
        var basicWallPositions = FindWallPositions(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallPositions(floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWalls(tilemapVisualiser, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualiser, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWalls(TilemapVisualiser tilemapVisualiser, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach(var position in cornerWallPositions){
            string neighboursBinaryType = "";
            foreach(var direction in Direction2D.eightDirectionsList){      //from top clockwise
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition)) neighboursBinaryType += "1";
                else neighboursBinaryType += "0";
            }
            tilemapVisualiser.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWalls(TilemapVisualiser tilemapVisualiser, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinaryType = "";
            foreach(var direction in Direction2D.cardinalDirectionsList){       //top -> down -> right -> left
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition)) neighboursBinaryType += "1";
                else neighboursBinaryType += "0";
            }
            tilemapVisualiser.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }
}
