using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : Movement
{
    [SerializeField] private Transform movePoint;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private GameObject enemies;
    [SerializeField] private int moveTime = 100;
    [SerializeField] private int attackTime = 100;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isFlipped = false;
    [SerializeField] private int attack;
    private Player player;
    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }
    public void MoveUp(InputAction.CallbackContext context)
    {
        if(context.performed){
            Vector3Int direction = new(0,1,0);
            TakeAction(direction);
        }
    }
    public void MoveDown(InputAction.CallbackContext context)
    {
        if(context.performed){
            Vector3Int direction = new(0,-1,0);
            TakeAction(direction);
        }
    }
    public void MoveLeft(InputAction.CallbackContext context)
    {
        if(context.performed){
            Vector3Int direction = new(-1,0,0);
            TakeAction(direction);
        }
    }
    public void MoveRight(InputAction.CallbackContext context)
    {
        if(context.performed){
            Vector3Int direction = new(1,0,0);
            TakeAction(direction);
        }
    }
    private void TakeAction(Vector3Int direction)
    {
        Vector3Int target = Vector3Int.FloorToInt(gameObject.transform.position) + direction;
        TileBase tile = wallTilemap.GetTile(target);
        foreach(Transform enemy in enemies.transform){
            if(enemy.position == target){
                Attack(enemy.GetComponent<Enemy>());
                Flip(direction);
                return;
            }
        }
        if(tile==null){
            GetComponent<Transform>().position += direction;
            movePoint.position -= direction;
            Move(animator, movePoint, gameObject.transform);
            Flip(direction);
            Clock.PassTime(moveTime);
        }
    }
    private void Flip(Vector3Int direction)
    {
        if((direction[0] > 0 && isFlipped) || (direction[0] < 0 && !isFlipped)){
                var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                spriteRenderer.flipX = !spriteRenderer.flipX;
                isFlipped = !isFlipped;
        } 
    }
    private void Attack(Enemy enemy)
    {
        Debug.Log("You attacked the " + enemy.gameObject.name + "!");
        enemy.Hurt(attack);
        Clock.PassTime(attackTime);
    }
}
