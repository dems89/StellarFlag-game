using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField]
    private Transform player; 

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.position;
            playerPosition.z = transform.position.z;
            transform.position = playerPosition;
        }
    }
}
