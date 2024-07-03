using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoorController : MonoBehaviour
{
    [SerializeField]
    private GameObject puerta; 

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            // Cambia el estado de la puerta
            puerta.SetActive(false);
           
        }
    }



}
