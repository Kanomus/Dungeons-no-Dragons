using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static void SpawnEnemies(HashSet<HashSet<Vector2Int>> rooms, int maxEnemiesPerRoom, GameObject[] enemyPrefabs, GameObject clones)
    {
        foreach(var room in rooms){
            int numberToSpawn = Random.Range(0, maxEnemiesPerRoom + 1);
            List<Vector2Int> possiblePositions = room.ToList<Vector2Int>();
            while(numberToSpawn>0){
                Vector2Int spawnPosition = possiblePositions[Random.Range(0, possiblePositions.Count)];
                GameObject clone = Instantiate(enemyPrefabs[Random.Range(0,enemyPrefabs.Length)], new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
                clone.transform.parent = clones.transform;
                possiblePositions.Remove(spawnPosition);
                numberToSpawn--;
            }
        }
    }

    public static void RemoveEnemies(GameObject clones){
        while(clones.transform.childCount > 0){
            DestroyImmediate(clones.transform.GetChild(0).gameObject);
        }
    }
}
