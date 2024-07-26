using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static GameManager Instance;
    private bool _isPaused = false;
    private PlayerController _pController;
    private Dictionary<string, int> enemyKillCount;
    private float _timer;
    private bool _isTimer = false;
    private bool canPause=true;
    private void Awake()
    {        
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }     
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        enemyKillCount = new Dictionary<string, int>
        {
            { "GreenEnemy", 0 },
            { "BlueEnemy", 0 },
            { "RedEnemy", 0 },
            { "Timer",0 }
        };
        _timer = 0f;
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
        if (_isTimer)
        {
            _timer += Time.deltaTime;
        }
    }

    public void UpdateEnemyCount(string enemyTag)
    {
        if (enemyKillCount.ContainsKey(enemyTag))
        {
            enemyKillCount[enemyTag]++;
        }
    }
    public int GetEnemyCount(string enemyTag)
    {
        if (enemyKillCount.ContainsKey(enemyTag))
        {
            return enemyKillCount[enemyTag];
        }
        return 0;
    }
    public void ResetStatics()
    {
        List<string> keys = new List<string>(enemyKillCount.Keys);
        foreach (string key in keys)
        {
            enemyKillCount[key] = 0;
        }
        _timer = 0f;
        _isTimer = false;
    }
    public void StartTimer()
    {
        _isTimer = true;
        _timer = 0f;
    }
    public void StopTimer()
    {
        _isTimer = false;
    }
    public void ResumeTimer()
    {
        _isTimer = true;
    }
    public float GetTimer()
    {
        return _timer;
    }
    
    public string RankCalculate()
    {
        sbyte currentLife = LifeManager.Instance.GetLife(); 
        float currentTime = GetTimer();

        if (currentTime <= 420f && (currentLife == 2))
        {
            return "S";
        }if (currentTime <= 450 && currentLife == 1)
        {
            return "A";
        }if (currentTime <= 550 && currentLife <= 1)
        {
            return "B";
        }else return "C";
        
    }
    void TogglePause()
    {
        if (canPause)
        {
            _isPaused = !_isPaused;
            if (_isPaused)
            {
                PauseGame();
                StopTimer();
            }
            else
            {
                ResumeGame();
                ResumeTimer();
            }
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
    public void SetCanPause(bool pause)
    {
        canPause = pause;
    }
}
