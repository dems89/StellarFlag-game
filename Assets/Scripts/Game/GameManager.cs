using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject inGameHUD;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    private GameObject player;
    public GameObject victoryHUD;
    public static GameManager instance;
    private bool isPaused = false, isGameMenu = true;
    private int currentLevel = 0;
    private int totalLevels = 3;

    private void Awake()
    {        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        if (isGameMenu)
        {
            isPaused = false;
            inGameHUD.SetActive(false);
            pauseMenu.SetActive(false);
            victoryHUD.SetActive(false);
            mainMenu.SetActive(true);
        }
        else { inGameHUD.SetActive(true); mainMenu.SetActive(false);  }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu")
        {
            FindPlayer();
        }
    }
    private void FindPlayer()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player no encontrado. Asegúrate de que hay un objeto con la etiqueta 'Player' en la escena.");
        }
    }
    public void ResumeGame()
    {
        if (currentLevel != 0)
        {
            player.GetComponent<PlayerController>().ChangeShootCap(true);
            pauseMenu.SetActive(false);
            inGameHUD.SetActive(true);
            Time.timeScale = 1f;
            isPaused = false;
        }
       
    }

    public void PauseGame()
    {
        if (currentLevel != 0)
        {
            player.GetComponent<PlayerController>().ChangeShootCap(false);
            pauseMenu.SetActive(true);
            inGameHUD.SetActive(false);
            Time.timeScale = 0f;
            isPaused = true;
        }
          
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        currentLevel = 0;
        SceneManager.LoadScene("MainMenu");
        isGameMenu = true;
    }

    public void QuitGame()
    {
            Application.Quit(); 
    }

    public void NextLevel()
    {
        if (currentLevel <= totalLevels)
        {
            currentLevel++;
            SceneManager.LoadScene("Level" + currentLevel);
            isGameMenu = false;
        }

    }
    public void SelectLevel(int level)
    {
        isGameMenu = false;
        SceneManager.LoadScene("level"+ level);
        currentLevel = level;
    }
    public void UpdatePlayerHealth(int health)
    {    
        inGameHUD.GetComponent<UIController>().SetHealth(health);
    }
    public void SetMaxPlayerHealth(int maxHealth)
    {
        inGameHUD.GetComponent<UIController>().SetMaxHealth(maxHealth);
    }

}
