using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    private GameObject player;
    private Camera m_camera;

    private void Awake()
    {
        m_camera = Camera.main;
        m_camera.orthographicSize = 13f;
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
}
