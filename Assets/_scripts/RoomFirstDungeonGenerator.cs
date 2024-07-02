using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;



public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int minHeight = 4, minWidth = 4;
    [SerializeField] private int dungeonHeight = 20, dungeonWidth = 20;
    [SerializeField] [Range(0,10)] private int offset = 1;
    [SerializeField] private bool RandomWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        tilemapVisualiser.Clear();
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt((Vector3Int)startPosition, new Vector3Int (dungeonWidth, dungeonHeight, 0)),
            minWidth, minHeight
        );
        
        HashSet<HashSet<Vector2Int>> rooms = new();
        if(RandomWalkRooms) rooms = CreateRandomRooms(roomsList);
        else rooms = CreateSimpleRooms(roomsList);
        
        HashSet<Vector2Int> floor = new();
        foreach(var room in rooms){
            foreach(var position in room){
                floor.Add(position);
            }
        }

        SpawnPlayer(roomsList[0]);

        List<Vector2Int> roomCenters = new();
        foreach(var room in roomsList){
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilemapVisualiser.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualiser);
        
        EnemySpawner.RemoveEnemies(enemyClones);
        EnemySpawner.SpawnEnemies(rooms, maxEnemiesPerRoom, enemyPrefabs, enemyClones);
    }

    private void SpawnPlayer(BoundsInt spawnRoom)
    {
        Transform player = GameObject.Find("Player").transform;
        player.position = Vector3Int.RoundToInt(spawnRoom.center);
    }

    private HashSet<HashSet<Vector2Int>> CreateRandomRooms(List<BoundsInt> roomsList)
    {
        HashSet<HashSet<Vector2Int>> rooms = new();
        
        for(int i=0; i<roomsList.Count; i++){
            var roomBounds = roomsList[i];
            
            Vector2Int roomCenter = new(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y)); //(Vector2Int)Vector3Int.RoundToInt(roomBounds.center);
            
            HashSet<Vector2Int> floor = new();
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            foreach(var position in roomFloor){
                if(
                    position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) &&
                    position.y >= (roomBounds.yMin + offset) && position.y <= (roomBounds.yMax - offset)
                ) floor.Add(position);
            }
            rooms.Add(floor);
        }
        return rooms;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while(roomCenters.Count > 0){
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new();
        var position = currentRoomCenter;
        corridor.Add(position);
        while(position.y != destination.y){
            if(destination.y > position.y){
                position += Vector2Int.up;
            } else if(destination.y < position.y){
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while(position.x != destination.x){
            if(destination.x > position.x){
                position += Vector2Int.right;
            } else if(destination.x < position.x){
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float length = float.MaxValue;
        foreach(var position in roomCenters){
            float currentdistance = Vector2.Distance(position, currentRoomCenter);
            if(currentdistance < length){
                length = currentdistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<HashSet<Vector2Int>> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<HashSet<Vector2Int>> rooms = new();

        foreach (var room in roomsList){
            HashSet<Vector2Int> floor = new();
            for(int col = offset; col < room.size.x - offset; col++){
                for(int row = offset; row < room.size.y - offset; row++){
                    Vector2Int position = (Vector2Int) room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
            rooms.Add(floor);
        }
        return rooms;
    }
}
