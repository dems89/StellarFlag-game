using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float _speed = 50f;
    private Vector3 _direction;
    private Rigidbody2D _rb;
    readonly WaitForSeconds delay = new WaitForSeconds(2);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();        
        player = GameObject.FindWithTag("Player");       
    }
    private void OnEnable()
    {
        ShootDirection();
        StartCoroutine(DisableAfterTime());
    }
    void FixedUpdate()
    {
        Movement();        
    }

    private void Movement()
    {
        _rb.MovePosition(transform.position + _direction *  _speed * Time.fixedDeltaTime);     
    }
    public void ShootDirection()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
        _direction = (targetPosition - player.transform.position).normalized;
    }
    private IEnumerator DisableAfterTime()
    {
        yield return delay;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
      if (!collision.collider.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
            
        
    }
}