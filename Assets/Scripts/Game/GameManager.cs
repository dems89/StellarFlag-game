using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static GameManager Instance;
    private bool _isPaused = false;
    private PlayerController _pController;
    private void Awake()
    {        
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }     
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GetSceneName() != "MainMenu")
        {
            TogglePause();
        }
        if (GetSceneName() != "MainMenu" && _pController == null)
        {
            _pController = FindObjectOfType<PlayerController>();
        }
    }
    void TogglePause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            PauseGame();            
        }
        else
        {
            ResumeGame();
        }
    }
    public void PauseGame()
    {        
        HUDManager.Instance.SetHUD(HUDType.PauseMenu);
        Time.timeScale = 0f;
        if (_pController != null)
        {
            _pController.ChangeShootCap(false);
        }
    }
    public void ResumeGame()
    {
        HUDManager.Instance.SetHUD(HUDType.InGame);
        Time.timeScale = 1f;
        if (_pController != null)
        {
            _pController.ChangeShootCap(true);
        }
    }
    private string GetSceneName()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        return sceneName;
    }
    public void SetPaused (bool isPaused)
    {
        _isPaused = isPaused;
    }
}
