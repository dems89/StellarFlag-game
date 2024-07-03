using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    private GameObject player;
    private Camera m_camera;
    private float duration = 1.0f;

    private void Awake()
    {
        m_camera = Camera.main;
        player = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            playerPosition.z = transform.position.z;
            transform.position = playerPosition;
        }
    }

    public void SetCameraSize(float targetSize)
    {
        StartCoroutine(SmoothChangeCameraSize(targetSize));
    }

    private IEnumerator SmoothChangeCameraSize(float targetSize)
    {
        float startSize = m_camera.orthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            m_camera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / duration);
            yield return null;
        }

        m_camera.orthographicSize = targetSize;
    }

}
