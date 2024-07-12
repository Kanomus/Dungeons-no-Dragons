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
    public void MoveUp(InputAction.CallbackContext context)
    {
        if(context.performed){
            Vector3Int direction = new(0,1,0);
            Vector3Int target = Vector3Int.FloorToInt(movePoint.position) + direction;
            TileBase tile = wallTilemap.GetTile(target);
            bool toAttack = false;
            foreach(Transform enemy in enemies.transform){
                if(enemy.position == target) toAttack = true;
            }
            if(toAttack) Attack();
            else if(tile==null){
                Move(animator, movePoint, direction);
                Clock.PassTime(moveTime);
            }
        }
    }
    public void MoveDown(InputAction.CallbackContext context)
    {
        if(context.performed){
            Vector3Int direction = new(0,-1,0);
            Vector3Int target = Vector3Int.FloorToInt(movePoint.position) + direction;
            TileBase tile = wallTilemap.GetTile(target);
            bool toAttack = false;
            foreach(Transform enemy in enemies.transform){
                if(enemy.position == target) toAttack = true;
            }
            if(toAttack) Attack();
            else if(tile==null){
                Move(animator, movePoint, direction);
                Clock.PassTime(moveTime);
            }
        }
    }
    public void MoveLeft(InputAction.CallbackContext context)
    {
        if(context.performed){
            Vector3Int direction = new(-1,0,0);
            Vector3Int target = Vector3Int.FloorToInt(movePoint.position) + direction;
            TileBase tile = wallTilemap.GetTile(target);
            bool toAttack = false;
            foreach(Transform enemy in enemies.transform){
                if(enemy.position == target) toAttack = true;
            }
            if(toAttack) Attack();
            else if(tile==null){
                Move(animator, movePoint, direction);
                Clock.PassTime(moveTime);
            }
        }
    }
    public void MoveRight(InputAction.CallbackContext context)
    {
        if(context.performed){
            Vector3Int direction = new(1,0,0);
            Vector3Int target = Vector3Int.FloorToInt(movePoint.position) + direction;
            TileBase tile = wallTilemap.GetTile(target);
            bool toAttack = false;
            foreach(Transform enemy in enemies.transform){
                if(enemy.position == target) toAttack = true;
            }
            if(toAttack) Attack();
            else if(tile==null){
                Move(animator, movePoint, direction);
                Clock.PassTime(moveTime);
            }
        }
    }
    
    private void Attack()
    {
        Debug.Log("You attacked the enemy!");
        Clock.PassTime(attackTime);
    }
}
