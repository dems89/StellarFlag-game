using UnityEngine;

public class MapCamera : MonoBehaviour
{
    private GameObject _player;

    private void Start()
    {        
       _player = GameObject.FindWithTag("Player");
        
    }
    private void Update()
    {
        transform.position = _player.transform.position;
        Debug.Log(transform.position);
        Debug.Log(_player.transform.position);
    }
}
