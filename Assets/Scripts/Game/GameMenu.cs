using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject gameHUD;
    public GameObject pauseMenu;
    public GameObject player;

    private bool isPaused = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {           
            if (isPaused)
            {               
                ResumeGame();
            }
            else
            {               
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        player.GetComponent<PlayerController>().ChangeShootCap(true);
        pauseMenu.SetActive(false);
        gameHUD.SetActive(true);
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void PauseGame()
    {
        player.GetComponent<PlayerController>().ChangeShootCap(false);
        pauseMenu.SetActive(true);
        gameHUD.SetActive(false);
        Time.timeScale = 0f; 
        isPaused = true;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
            Application.Quit(); 
    }
   
}
