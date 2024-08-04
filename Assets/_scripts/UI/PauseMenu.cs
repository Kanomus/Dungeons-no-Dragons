using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public bool isPaused = false;
    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = isPaused ? 0 : 1;
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
    }
    public void Quit()
    {
        Scene startMenu = SceneManager.GetSceneByName("StartMenu");
        if(!startMenu.isLoaded)
            SceneManager.LoadScene("StartMenu");
        SceneManager.SetActiveScene(startMenu);
    }
}
