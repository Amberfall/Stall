using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu; 

    void Update()
    {
        
    }
    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
