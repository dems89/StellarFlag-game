using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraSize : MonoBehaviour
{
    public Camera mainCamera;
    public int cameraSize;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player"))
        {
            mainCamera.GetComponent<CameraTracking>().SetCameraSize(cameraSize);
        }
    }
}
