using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxhp = 20;
    public int hp;
    [SerializeField] private GameObject healthBar;

    void Start()
    {
        hp = maxhp;
        SetMaxHealth(maxhp);
    }
    public void SetMaxHealth(int health)
    {
        maxhp = health;
        healthBar.GetComponent<Slider>().maxValue = health;
    }
    void Update()
    {
        healthBar.GetComponent<Slider>().value = hp;
        try{healthBar.GetComponentInChildren<TextMeshProUGUI>().text = hp.ToString() + " / " + maxhp.ToString();}
        catch{Debug.Log("TExt not FouND");}
        
    }
}
