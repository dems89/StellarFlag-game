using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance { get; private set; }
    public byte lifes = 2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
          
        }else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void SetLife(byte life)
    {
        lifes = life;
    }
    public byte GetLife()
    {
        return lifes;
    }
    public bool IsAlive()
    {
        return lifes > 0;
    }
    public void DecreaseLife()
    {
        if (IsAlive())
        {
            lifes--;
            HUDManager.Instance.UpdateUIHealthIcon();
        }       
    }
    public void ResetLife()
    {
        lifes = 2;
        HUDManager.Instance.UpdateUIHealthIcon();
    }
}
