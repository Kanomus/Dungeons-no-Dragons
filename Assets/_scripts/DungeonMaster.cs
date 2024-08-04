using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    public GameObject enemyClones;
    private List<EnemyMovement> enemies = new();
    [SerializeField] private RoomFirstDungeonGenerator dungeonGenerator;
    [SerializeField] private GameObject winScreen;
    private void Start()
    {
        dungeonGenerator.GenerateDungeon();
        FindEnemies();
    }
    public void FindEnemies()
    {
        enemies = new();
        foreach(Transform enemy in enemyClones.transform){
            enemies.Add(enemy.GetComponent<EnemyMovement>());
        }
    }
    public void EnemyTurn(int timeToPass)
    {
        FindEnemies();
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
    public void Descend()
    {
        Victory();
    }
    private void Victory()
    {
        Debug.Log("You win!!");
        winScreen.SetActive(true);
    }
}
