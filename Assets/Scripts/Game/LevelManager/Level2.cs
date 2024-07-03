using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> planets;
    private short countCaptured = 0;
    private string totalPlanets;
    [SerializeField]
    private TMPro.TextMeshProUGUI planetsCaptured;
    


    private void Awake()
    {
        AddToPlanetList();
        totalPlanets = planets.Count.ToString();      
    }
    private void Start()
    {
        planetsCaptured.text = $"Planetas Liberados: {countCaptured} / {totalPlanets}";
    }

    private void OnEnable()
    {
        Enemy_Spawner.OnPlanetCaptured += OnPlanetCaptured;
    }
    private void OnDisable()
    {
        Enemy_Spawner.OnPlanetCaptured -= OnPlanetCaptured;
    }
    private void AddToPlanetList()
    {
        // Encuentra todos los GameObjects con el layer "Planets"
        foreach (GameObject planet in FindObjectsOfType<GameObject>())
        {
            if (planet.layer == LayerMask.NameToLayer("Planets"))
            {
                planets.Add(planet);
            }
        }
    }

    private void OnPlanetCaptured(GameObject planet)
    {
        countCaptured++;
        planetsCaptured.text = $"Planetas Liberados: {countCaptured} / {totalPlanets}";

        if( countCaptured == planets.Count)
        {
            HUDManager.Instance.SetHUD(HUDType.Victory);
            gameObject.SetActive(false);
        }
    }
}
