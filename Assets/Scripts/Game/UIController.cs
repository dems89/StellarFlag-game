using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private TMPro.TextMeshProUGUI _healthText;
    [SerializeField]
    private Image[] lifeimages;

    public void SetHealth(int health)
    {
        healthBar.value = health;
        _healthText.text = health.ToString();
    }
    public void SetMaxHealth(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
    }
    public void UpdateLifeUI()
    {
        int currentLife = LifeManager.Instance.GetLife();

        for (int i = 0; i < lifeimages.Length; i++)
        {          
            if (i <= currentLife)
            {         
                SetImageOpacity(lifeimages[i], 1f);
            }else
            {
                SetImageOpacity(lifeimages[i], 0.1f);
            }                  
        }
    }
    private void SetImageOpacity(Image image, float opacity)
    {
        Color color = image.color;
        color.a = opacity;
        image.color = color;
    }

}
