using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject HUDPrefab;
    [SerializeField]
    private GameObject audioManager;


    private static bool isHUDInitialized = false;

    void Awake()
    {
        if (!isHUDInitialized)
        {
            GameObject auManager = Instantiate(audioManager);
            GameObject hudInstance = Instantiate(HUDPrefab);
            DontDestroyOnLoad(hudInstance);
            DontDestroyOnLoad(auManager);
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
