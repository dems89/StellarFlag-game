using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public TMPro.TextMeshProUGUI red;
    public TMPro.TextMeshProUGUI blue;
    public TMPro.TextMeshProUGUI green;
    public TMPro.TextMeshProUGUI timer;
    public TMPro.TextMeshProUGUI remainingLife;
    public TMPro.TextMeshProUGUI rank;

    private void OnEnable()
    {
        UpdateStatistics();
    }

    private void OnDisable()
    {
        ClearStatistics();
    }

    private void UpdateStatistics()
    {
        red.text = GameManager.Instance.GetEnemyCount("RedEnemy").ToString();
        blue.text = GameManager.Instance.GetEnemyCount("BlueEnemy").ToString();
        green.text = GameManager.Instance.GetEnemyCount("GreenEnemy").ToString();
        timer.text = GameManager.Instance.GetTimer().ToString("F1") + " seg";
        remainingLife.text = "x " + (LifeManager.Instance.GetLife() +1 ).ToString();
        rank.text = GameManager.Instance.RankCalculate();
    }

    private void ClearStatistics()
    {
        red.text = "";
        blue.text = "";
        green.text = "";
        timer.text = "";
        remainingLife.text = "";
        rank.text = "";
    }
}
