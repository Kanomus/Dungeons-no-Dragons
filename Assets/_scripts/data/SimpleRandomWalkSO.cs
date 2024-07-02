using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "simpleRandomWalkParameters_", menuName = "PCG/simpleRandomWalkData")]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations=10, walkLength=10;
    public bool startRandomlyEachIteration=false;
}
