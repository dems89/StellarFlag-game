using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject HUDPrefab;
    private static bool isHUDInitialized = false;

    void Awake()
    {
        if (!isHUDInitialized)
        {
            GameObject hudInstance = Instantiate(HUDPrefab);
            DontDestroyOnLoad(hudInstance);
            isHUDInitialized = true;
        }
        CheckForDuplicateHUDs();
    }

    void CheckForDuplicateHUDs()
    {
        GameObject[] huds = GameObject.FindGameObjectsWithTag("HUD");
        if (huds.Length > 1)
        {
            for (int i = 1; i < huds.Length; i++)
            {
                Destroy(huds[i]);
            }
        }
    }
}
