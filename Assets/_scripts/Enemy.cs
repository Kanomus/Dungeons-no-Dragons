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

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        maxHP = enemyData.maxHP;
        SetMaxHealth(maxHP);
    }
    public void SetMaxHealth(int health)
    {
        maxHP = health;
        // healthBar.maxValue = health;
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
    }
}
