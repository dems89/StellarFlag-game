using UnityEngine;

public class SwitchTurret : MonoBehaviour
{
    [SerializeField]
    public Canvas missionComplete;
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
        if (Input.GetKeyUp(KeyCode.E) && _status == true)
        {
            missionComplete.GetComponent<Canvas>().enabled = true;
        }
    }
}