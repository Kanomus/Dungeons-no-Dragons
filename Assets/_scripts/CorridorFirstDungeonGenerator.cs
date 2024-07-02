using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UIElements;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLength = 17, corridorCount = 4;
    [SerializeField] [Range(0.1f, 1)] private float roomPercent = 0.8f;
    
    protected override void RunProceduralGeneration()
    {
        tilemapVisualiser.Clear();
        CorridorFirstGeneration();
    }
    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);
        HashSet<HashSet<Vector2Int>> rooms = CreateRooms(potentialRoomPositions);
        HashSet<Vector2Int> roomPositions = new();
        foreach(var room in rooms){
            roomPositions.UnionWith(room);
        }
        List<Vector2Int> deadends = FindDeadends(floorPositions);
        CreateRoomsAtDeadends(deadends, floorPositions);

        floorPositions.UnionWith(roomPositions);

        for(int i=0; i<corridors.Count; i++){
            corridors[i] = IncreaseCorridorSizeBy1(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }
        
        tilemapVisualiser.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualiser);
        EnemySpawner.RemoveEnemies(enemyClones);
        EnemySpawner.SpawnEnemies(rooms, maxEnemiesPerRoom, enemyPrefabs, enemyClones);
    }

    private List<Vector2Int> IncreaseCorridorSizeBy1(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;
        for(int i=1; i<corridor.Count; i++){
            Vector2Int direction = corridor[i] - corridor[i-1];
            if(previousDirection!=Vector2Int.zero && direction!=previousDirection){
                //corner
                newCorridor.Add(corridor[i-1] + previousDirection);
                previousDirection=direction;
            }
            else{
                //non corner
                Vector2Int newtileoffset = GetDirection90From(direction);
                newCorridor.Add(corridor[i-1]);
                newCorridor.Add(corridor[i-1]+newtileoffset);
            }
        }
        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        Vector2Int newDirection = new Vector2Int();
        newDirection.x = -direction.y;
        newDirection.y = direction.x;
        return newDirection;
    }

    private List<Vector2Int> FindDeadends(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadends = new List<Vector2Int>();
        foreach(var position in floorPositions){
            int neighborCount = 0;
            foreach(var direction in Direction2D.cardinalDirectionsList){
                if(floorPositions.Contains(position+direction)) neighborCount++;
            }
            if(neighborCount==1) deadends.Add(position);
        }     
        return deadends;   
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for(int i=0; i<corridorCount; i++){
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
        return corridors;
    }

    private HashSet<HashSet<Vector2Int>> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<HashSet<Vector2Int>> rooms = new();
        int roomCount = Mathf.RoundToInt(potentialRoomPositions.Count*roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x=>Guid.NewGuid()).Take(roomCount).ToList();

        foreach(var roomPosition in roomsToCreate){
            var room = RunRandomWalk(randomWalkParameters, roomPosition);
            rooms.Add(room);
        }

        return rooms;
    }
    private void CreateRoomsAtDeadends(List<Vector2Int> deadends, HashSet<Vector2Int> floorPositions)
    {
        foreach(var position in deadends){
            if(floorPositions.Contains(position)==false){
                var room = RunRandomWalk(randomWalkParameters, position);
                floorPositions.UnionWith(room);
            }
        }
    }
}
