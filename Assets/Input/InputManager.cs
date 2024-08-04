using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class InputManager : MonoBehaviour
{
    private GameObject pauseMenu;
    public bool isPaused = false;
    public void TogglePause(InputAction.CallbackContext callbackContext)
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        if(callbackContext.performed){
            if(isPaused) Unpause();
            else Pause();
        }
    }
    private void Unpause()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }
    private void Pause()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(true);
    }
}
