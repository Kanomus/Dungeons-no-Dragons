using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
    protected int hp = 0;
    protected float MoveSpeed = 0f;
    protected LayerMask collisionLayer;
}
