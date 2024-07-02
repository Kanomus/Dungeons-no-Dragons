using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemyMovement : Movement
{
    private Transform movePoint;
    private Tilemap wallTilemap;
    private GameObject enemies;
    private GameObject player;
    public EnemyData enemyData;
    public int waitTime;
    private void Start()
    {
        movePoint = GetComponent<Transform>();
        enemies = GameObject.Find("EnemyClones");
        player = GameObject.Find("Player");
        wallTilemap = TilemapManager.Instance.wallTilemap;
        waitTime = enemyData.moveTime;
        if(wallTilemap == null) Debug.Log("No Tilemap found");
    }
    public void ChooseRandomDirection()
    {
        Vector2Int startPosition = Vector2Int.RoundToInt(movePoint.position);
        bool blocked = false;
        int attempts = 0;
        Vector2Int direction = new();
        Vector3Int destination = new();
        do{
            blocked = false;
            direction = Direction2D.GetRandomCardinalDirection();
            destination = (Vector3Int)(startPosition + direction);
            TileBase wall = wallTilemap.GetTile(destination);
            //repeat this if tile!=null || enemy.position == destination
            foreach(Transform enemy in enemies.transform){
                if(enemy.position == destination) blocked = true;
            }
            if(wall != null) blocked = true;
            attempts++;
        } while(blocked && attempts<10);
        
        if(player.transform.position == (Vector3)destination){
            Attack();
            return;
        }
        else if(!blocked){
            Move(movePoint, (Vector3Int)direction);
            return;
        }
        
    }

    private void Attack()
    {
        Debug.Log(name + " attacked you!");
    }
}
