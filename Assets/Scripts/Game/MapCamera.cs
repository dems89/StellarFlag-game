using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _pIcon;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");        
    }

    private void Update()
    {
        RedDotMovement();
        //Debug.Log(_pIcon.transform.position);
        //Debug.Log(_player.transform.position);
    }

    private void RedDotMovement()
    {
        _pIcon.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, 0f);
    }
}
