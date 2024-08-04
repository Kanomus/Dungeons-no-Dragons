using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TilemapVisualiser tilemapVisualiser = null;
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField] protected int maxEnemiesPerRoom;
    [SerializeField] protected GameObject enemyClones;
    [SerializeField] protected GameObject[] enemyPrefabs;

    protected abstract void RunProceduralGeneration();
    public void GenerateDungeon(){
        RunProceduralGeneration();
    }

}
