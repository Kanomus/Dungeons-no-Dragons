using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void Restart()
    {
        this.gameObject.SetActive(false);
        GameObject.Find("Player").GetComponent<Player>().Revive();
    }
    public void Quit()
    {
        Scene startMenu = SceneManager.GetSceneByName("StartMenu");
        if(!startMenu.isLoaded)
            SceneManager.LoadScene("StartMenu");
        SceneManager.SetActiveScene(startMenu);
    }
}
