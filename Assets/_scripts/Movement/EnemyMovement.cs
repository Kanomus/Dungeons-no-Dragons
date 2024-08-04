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
    private Animator animator;
    [SerializeField] private bool isFlipped;
    private void Start()
    {
        movePoint = this.gameObject.transform.GetChild(0).transform;
        enemies = GameObject.Find("EnemyClones");
        player = GameObject.Find("Player");
        // wallTilemap = TilemapManager.Instance.wallTilemap;
        wallTilemap = GameObject.Find("TilemapVisualiser").GetComponent<TilemapVisualiser>().wallTilemap;
        waitTime = enemyData.moveTime;
        if(wallTilemap == null) Debug.Log("No Tilemap found");
        animator = GetComponentInChildren<Animator>();
    }
    public void ChooseRandomDirection()
    {
        Vector2Int startPosition = Vector2Int.RoundToInt(movePoint.position);
        bool blocked;
        int attempts = 0;
        _ = new Vector2Int();
        _ = new Vector3Int();
        Vector2Int direction;
        Vector3Int destination;
        do
        {
            blocked = false;
            direction = Direction2D.GetRandomCardinalDirection();
            destination = (Vector3Int)(startPosition + direction);
            TileBase wall = wallTilemap.GetTile(destination);
            //repeat this if tile!=null || enemy.position == destination
            foreach (Transform enemy in enemies.transform)
                if (enemy.position == destination) blocked = true;

            if (wall != null) blocked = true;
            attempts++;
        } while (blocked && attempts < 10);

        if (player.transform.position == (Vector3)destination){
            Attack();
            Flip(direction);
            return;
        }
        else if(!blocked){
            gameObject.transform.position += (Vector3Int) direction;
            movePoint.position -= (Vector3Int) direction;
            Move(animator, movePoint, gameObject.transform);
            Flip(direction);
            return;
        }
        return;
    }
    private void Flip(Vector2Int direction)
    {
        if((isFlipped && direction[0] > 0) || (!isFlipped && direction[0] < 0)){
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.flipX = !spriteRenderer.flipX;
            isFlipped = !isFlipped;
        }
    }

    private void Attack()
    {
        Debug.Log(name + " attacked you!");
        animator.SetTrigger("Attack");
        player.GetComponent<Player>().hp -= enemyData.damage;
    }
}
