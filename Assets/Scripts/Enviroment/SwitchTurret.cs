using UnityEngine;

public class SwitchTurret : MonoBehaviour
{
    [SerializeField]
    private GameObject Laser;
    private bool _status = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _status = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _status = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _status == true)
        {
            Debug.Log("Espacio apretado");
            Laser.SetActive(!Laser.activeSelf);
        }
    }
}