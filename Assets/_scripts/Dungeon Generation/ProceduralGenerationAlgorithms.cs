using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var previousPosition = startPosition;

        for(int i=0; i<walkLength; i++){
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition=newPosition;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        path.Add(startPosition);

        for(int i=0; i<corridorLength; i++){
            path.Add(path[i] + direction);
        }
        return path;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new();
        List<BoundsInt> roomsList = new();

        roomsQueue.Enqueue(spaceToSplit);

        while(roomsQueue.Count > 0){
            BoundsInt room = roomsQueue.Dequeue();
            if(room.size.x >= minWidth && room.size.y >= minHeight){
                
                if(Random.value > 0.5f){
                    //split horizontally preferentilly
                    if(room.size.x >= minWidth * 2){
                        SplitVertically(minWidth, roomsQueue, room);
                    } else if(room.size.y >= minHeight * 2){
                        SplitHorizontally(minHeight, roomsQueue, room);
                    } else{
                        roomsList.Add(room);
                    }
                } else {
                    //split vertically preferentially
                    if(room.size.y >= minHeight * 2){
                        SplitHorizontally(minHeight, roomsQueue, room);
                    } else if(room.size.x >= minWidth * 2){
                        SplitVertically(minWidth, roomsQueue, room);
                    } else{
                        roomsList.Add(room);
                    }
                }
            }
        }

        return roomsList;
    }

    private static void SplitHorizontally(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);

        BoundsInt room1 = new BoundsInt(
            room.min,
            new Vector3Int(room.size.x, ySplit, room.size.z)
        );

        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z)
        );
        
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitVertically(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);

        BoundsInt room1 = new BoundsInt(
            room.min,
            new Vector3Int(xSplit, room.size.y, room.size.z)
        );

        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z)
        );

        if(room1.size.y >= minHeight){
            roomsQueue.Enqueue(room1);
        }
        if(room2.size.y >= minHeight){
            roomsQueue.Enqueue(room2);
        }
    }
}


public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>{

        new Vector2Int(0,1),    //UP
        new Vector2Int(0,-1),   //DOWN
        new Vector2Int(1,0),    //RIGHT
        new Vector2Int(-1,0),   //LEFT
    };
    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>{

        new Vector2Int(1,1),    //UP-RIGHT
        new Vector2Int(1,-1),   //DOWN-RIGHT
        new Vector2Int(-1,-1),  //DOWN-LEFT
        new Vector2Int(-1,1),   //UP-LEFT
    };
    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>{

        new Vector2Int(0,1),    //UP
        new Vector2Int(1,1),    //UP-RIGHT
        new Vector2Int(1,0),    //RIGHT
        new Vector2Int(1,-1),   //DOWN-RIGHT
        new Vector2Int(0,-1),   //DOWN
        new Vector2Int(-1,-1),  //DOWN-LEFT
        new Vector2Int(-1,0),   //LEFT
        new Vector2Int(-1,1),   //UP-LEFT
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0,cardinalDirectionsList.Count)];
    }
}