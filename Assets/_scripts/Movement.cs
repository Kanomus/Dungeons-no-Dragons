using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public void Move(Transform movePoint, Vector3Int direction)
    {
        Vector3 destination = movePoint.position + direction;
        StartCoroutine(SmoothMovement(movePoint, destination));
    }
    IEnumerator SmoothMovement(Transform movePoint, Vector3 destination)
    {
        float moveSpeed = 0.1f;
        while(Vector3.Distance(movePoint.position, destination) >= 0.1f){
            movePoint.position = Vector3.Lerp(movePoint.position, destination, moveSpeed * Time.fixedDeltaTime);
        }
        movePoint.position = destination;
        yield return null;
    }
}
