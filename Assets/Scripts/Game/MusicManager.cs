using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioClip mainMenuMusic;
    public AudioClip lvl1;
    public AudioClip lvl2;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenu":
                PlayMusic(mainMenuMusic);
                break;
            case "Level1":
                PlayMusic(lvl1);
                break;
            case "Level2":
                PlayMusic(lvl2);
                break;
            default:
                audioSource.Stop(); // Detener la música si no se asigna ninguna
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip)
        {
            return; // Ya se está reproduciendo esta música
        }

        audioSource.clip = clip;
        audioSource.Play();
    }
}
