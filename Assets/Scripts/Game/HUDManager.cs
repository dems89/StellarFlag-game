using UnityEngine;
using UnityEngine.SceneManagement;

public enum HUDType
{
    InGame, MainMenu, PauseMenu, Victory, Defeat
}
public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inGameHUD;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject victoryHUD;
    [SerializeField]
    private GameObject defeatHUD;
    private int currentLevel = 0;
    private int totalLevels = 3;

    public static HUDManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
           SetHUD(HUDType.MainMenu);
        }
        else
        {
            SetHUD(HUDType.InGame);
        }
    }

    public void SetHUD(HUDType hud)
    {
        //Debug.Log("Hud: " + hud.ToString());

        inGameHUD.SetActive(false);
        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
        victoryHUD.SetActive(false);
        defeatHUD.SetActive(false);

        switch (hud)
        {
            case HUDType.InGame: inGameHUD.SetActive(true); break;
            case HUDType.PauseMenu: pauseMenu.SetActive(true); break;
            case HUDType.MainMenu: mainMenu.SetActive(true); GameManager.Instance.SetPaused(false); break;
            case HUDType.Victory: victoryHUD.SetActive(true); break;
            case HUDType.Defeat: defeatHUD.SetActive(true); break;
        }
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        currentLevel = 0;
        SceneManager.LoadScene("MainMenu");
        LifeManager.Instance.ResetLife();
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
        }
    }
    public void RepeatLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel);
    }
    public void SelectLevel(int level)
    {
        SceneManager.LoadScene("level" + level);
        currentLevel = level;
    }   
    public void UpdateUIplayerHealth(int health)
    {
        inGameHUD.GetComponent<UIController>().SetHealth(health);
    }
    public void SetUImaxPlayerHealth(int maxHealth)
    {
        inGameHUD.GetComponent<UIController>().SetMaxHealth(maxHealth);
    }
    public void UpdateUIHealthIcon()
    {
        inGameHUD.GetComponent<UIController>().UpdateLifeUI();
        
    }

}
