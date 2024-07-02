using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 GetDirection()
    {
        Vector3 direction = new Vector3();
        direction.z = Input.mouseScrollDelta.y;
        return direction;
    }
    private void move(Vector3 moveDirection)
    {
        transform.position = moveDirection;
    }
}
