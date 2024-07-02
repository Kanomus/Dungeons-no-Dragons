using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemyData", menuName = "PCG/enemyData")]
public class EnemyData : ScriptableObject
{
    public int moveTime;
    public int attackTime;
    public int maxHP;
    public int viewRange;
}
