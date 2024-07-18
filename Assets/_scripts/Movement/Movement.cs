using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public void Move(Animator animator, Transform movePoint, Transform parent)
    {
        animator.SetBool("IsMoving", true);
        StartCoroutine(SmoothMovement(animator, movePoint, parent));
    }
    
    IEnumerator SmoothMovement(Animator animator, Transform movePoint, Transform destination)
    {
        float moveTime = 15f;
        while(Vector2.Distance(movePoint.position, destination.position) > 0.1f){
            movePoint.position = Vector2.Lerp(movePoint.position, destination.position, Time.fixedDeltaTime * moveTime);
            yield return new WaitForFixedUpdate();
        }
        movePoint.position = destination.position;
        animator.SetBool("IsMoving", false);
    }
}
