using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int maxHP;
    [SerializeField] private EnemyData enemyData;
    private Animator animator;
    private int HP;
    private DungeonMaster dungeonMaster;
#pragma warning restore IDE0052 // Remove unread private members

    void Start()
    {
        dungeonMaster = GameObject.Find("DungeonMaster").GetComponent<DungeonMaster>();
        animator = GetComponentInChildren<Animator>();
        
        Initialise();
    }
    private void Initialise()
    {
        SetMaxHealth(enemyData.maxHP);
        HP = maxHP;
    }
    public void SetMaxHealth(int health)
    {
        maxHP = health;
    }

    public void Hurt(int health)
    {
        HP -= health;
        if(HP <= 0) Die();
    }
    private void Die()
    {
        Debug.Log(gameObject.name + " died...");
        animator.SetTrigger("Death");
        Destroy(this.gameObject, 0.2f);
    }
}
