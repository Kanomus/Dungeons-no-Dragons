using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private readonly int maxhp = 20;
    public int hp;
    [SerializeField] private GameObject healthBar;

    void Start()
    {
        hp = maxhp;
        healthBar.GetComponent<Slider>().maxValue = maxhp;
    }

    void Update()
    {
        healthBar.GetComponent<Slider>().value = hp;
    }

    public void Hit()
    {
        
    }
}
