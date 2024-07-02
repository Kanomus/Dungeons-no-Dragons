using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    public GameObject enemyClones;
    private readonly List<EnemyMovement> enemies = new();
    private void Start()
    {
        foreach(Transform enemy in enemyClones.transform){
            enemies.Add(enemy.GetComponent<EnemyMovement>());
        }
    }
    public void EnemyTurn(int timeToPass)
    {
        for(int i = 0; i < enemies.Count; i++){
            enemies[i].waitTime -= timeToPass;
            if(enemies[i].waitTime <= 0){
                while(enemies[i].waitTime <= 0){
                    enemies[i].ChooseRandomDirection();
                    enemies[i].waitTime += enemies[i].enemyData.moveTime;
                }
            }
        }

    }
}
