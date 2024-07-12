using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public void Move(Animator animator, Transform movePoint, Vector3Int direction)
    {
        animator.SetBool("IsMoving", true);
        Vector3 destination = movePoint.position + direction;
        StartCoroutine(SmoothMovement(animator, movePoint, destination));
    }
    IEnumerator SmoothMovement(Animator animator, Transform movePoint, Vector3 destination)
    {
        float moveSpeed = 20f;
        while(Vector2.Distance(movePoint.position, destination) > 0.01f){
            movePoint.position = Vector2.Lerp(movePoint.position, destination, Time.fixedDeltaTime * moveSpeed);
            yield return null;
        }
        movePoint.position = destination;
        animator.SetBool("IsMoving", false);
    }
}
