using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> planets;
    private bool _checkVictory = true;

    private void Awake()
    {
        AddToPlanetList();
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

    private void Update()
    {
        if (IsAllPlanetsCaptured(ref planets) && _checkVictory)
        {
            _checkVictory = false;
            HUDManager.Instance.SetHUD(HUDType.Victory);
        }
    }

    private bool IsAllPlanetsCaptured(ref List<GameObject> planets)
    {
        // Recorre la lista de planetas y verifica si todos están capturados
        foreach (GameObject planet in planets)
        {
            Enemy_Spawner enemySpawner = planet.GetComponent<Enemy_Spawner>();
            if (enemySpawner != null && !enemySpawner.GetCaptured())
            {
                return false; // Si algún planeta no está capturado, devuelve false
            }
        }
        return true; // Todos los planetas están capturados
    }
}
