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
    public int keyCount;
    private TextMeshProUGUI keyCountUI;
    private TilemapVisualiser tilemapVisualiser;

    void Start()
    {
        hp = maxhp;
        SetMaxHealth(maxhp);
        keyCountUI = GameObject.Find("KeyCount").GetComponentInChildren<TextMeshProUGUI>();
        tilemapVisualiser = GameObject.Find("TilemapVisualiser").GetComponent<TilemapVisualiser>();
    }
    public void SetMaxHealth(int health)
    {
        maxhp = health;
        healthBar.GetComponent<Slider>().maxValue = health;
    }
    public bool OpenExit()
    {
        bool canOpen = false;
        if(keyCount > 0){
            canOpen = true;
            tilemapVisualiser.OpenExit();
            keyCount--;
        }
        return canOpen;
    }
    void Update()
    {
        healthBar.GetComponent<Slider>().value = hp;
        try{healthBar.GetComponentInChildren<TextMeshProUGUI>().text = hp.ToString() + " / " + maxhp.ToString();}
        catch{Debug.Log("TExt not FouND");}
        keyCountUI.text = "x " + keyCount.ToString();  
    }
}
