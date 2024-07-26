using UnityEngine;
using UnityEngine.SceneManagement;

public enum HUDType
{
    InGame, MainMenu, PauseMenu, Victory, Defeat, skip, endStats
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
    [SerializeField]
    private GameObject endStatsHUD;
    public GameObject skipHUD;
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
            GameManager.Instance.ResetStatics();
        }else
        {
            SetHUD(HUDType.InGame);            
        }

        if(scene.name == "Level0")
        {
            SetHUD(HUDType.skip);
        }
        if (scene.name == "Level1" || scene.name =="Level2")
        {
            GameManager.Instance.StartTimer();
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
        skipHUD.SetActive(false);
        endStatsHUD.SetActive(false);

        switch (hud)
        {
            case HUDType.skip: skipHUD.SetActive(true); GameManager.Instance.SetCanPause(false); break;
            case HUDType.InGame: inGameHUD.SetActive(true); GameManager.Instance.SetCanPause(true); Time.timeScale = 1f; break;
            case HUDType.PauseMenu: pauseMenu.SetActive(true); break;
            case HUDType.MainMenu: mainMenu.SetActive(true); GameManager.Instance.SetCanPause(false); Time.timeScale = 1f; break;
            case HUDType.Victory: victoryHUD.SetActive(true); GameManager.Instance.SetCanPause(false); Time.timeScale = 0f; break;
            case HUDType.Defeat: defeatHUD.SetActive(true); GameManager.Instance.SetCanPause(false); Time.timeScale = 0f; break;
            case HUDType.endStats: endStatsHUD.SetActive(true); GameManager.Instance.SetCanPause(false); Time.timeScale = 0f; break;
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
        if (currentLevel < totalLevels)
        {
            if (currentLevel == totalLevels)
            {
                GoToMainMenu();
            }if (currentLevel == 0)
            {
                SceneManager.LoadScene("Level" + currentLevel);
                currentLevel++;
            }
            else
            {
                currentLevel++;
                SceneManager.LoadScene("Level" + currentLevel);
            }
            
        }
    }
    public void RepeatLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel);
        LifeManager.Instance.ResetLife();
        inGameHUD.GetComponent<UIController>().UpdateLifeUI();    

    }
    public void SelectWeaponUI(int weapon)
    {
        inGameHUD.GetComponent<UIController>().UpdateWeaponUI(weapon);
    }
    public void SelectLevel(int level)
    {
        LifeManager.Instance.ResetLife();
        inGameHUD.GetComponent<UIController>().UpdateLifeUI();
        SceneManager.LoadScene("Level" + level);
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
