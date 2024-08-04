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
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject deathScreen;

    void Awake()
    {
        keyCountUI = GameObject.Find("KeyCount").GetComponentInChildren<TextMeshProUGUI>();
        tilemapVisualiser = GameObject.Find("TilemapVisualiser").GetComponent<TilemapVisualiser>();
        Initialise();
    }
    public void Initialise()
    {
        SetMaxHealth(maxhp);
        hp = maxhp;
        keyCount = 0;
    }
    public void Revive()
    {
        animator.SetTrigger("Restart");
        animator.ResetTrigger("Death");
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
    bool CheckDeath()
    {
        if(hp <= 0){
            hp = 0;
            return true;
        }
        return false;
    }
    void Update()
    {
        healthBar.GetComponent<Slider>().value = hp;
        try{healthBar.GetComponentInChildren<TextMeshProUGUI>().text = hp.ToString() + " / " + maxhp.ToString();}
        catch{Debug.Log("TExt not FouND");}
        if(CheckDeath()){
            animator.SetTrigger("Death");
            deathScreen.SetActive(true);
            StartCoroutine(Die());
        }
        keyCountUI.text = "x " + keyCount.ToString();  
    }
    IEnumerator Die()
    {
        animator.ResetTrigger("Restart");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
