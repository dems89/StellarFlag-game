using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private TMPro.TextMeshProUGUI _healthText;

    public void SetHealth(int health)
    {
        healthBar.value = health;
        _healthText.text = health.ToString();
    }
    public void SetMaxHealth(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
    }
}
