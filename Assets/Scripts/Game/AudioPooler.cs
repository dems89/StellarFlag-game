using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPooler : MonoBehaviour
{
    public static AudioPooler Instance;
    public GameObject audioPrefab;
    private Queue<GameObject> audioPool = new Queue<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        GameObject audioObject;
        if (audioPool.Count > 0)
        {
            audioObject = audioPool.Dequeue();
            if (audioObject == null)
            {
                audioObject = Instantiate(audioPrefab);
            }
            else
            {
                audioObject.SetActive(true);
            }
        }
        else
        {
            audioObject = Instantiate(audioPrefab);
        }

        audioObject.transform.position = position;
        AudioSource audioSource = audioObject.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        StartCoroutine(ReturnToPool(audioObject, clip.length));
    }

    private IEnumerator ReturnToPool(GameObject audioObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (audioObject != null)
        {
            audioObject.SetActive(false);
            audioPool.Enqueue(audioObject);
        }
    }
}
