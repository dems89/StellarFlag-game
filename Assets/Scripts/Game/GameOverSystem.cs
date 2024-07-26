using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverSystem : MonoBehaviour
{

    public TMPro.TextMeshProUGUI deadText;
    public GameObject retryBtn;
    public GameObject mainMenuBtn;

    private void OnEnable()
    {
        if (LifeManager.Instance.GetLife() >= 1)
        {
            mainMenuBtn.SetActive(false);
            retryBtn.SetActive(true);
            deadText.text = "Vidas restantes: " + LifeManager.Instance.GetLife().ToString();
        }
        else { mainMenuBtn.SetActive(true);retryBtn.SetActive(false); }
    }


}
