using Unity.Mathematics;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 30f;
    private Vector3 _direccionDisparo;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

    }
    public void Configurar(Vector3 direccion)
    {
        _direccionDisparo = direccion;
        Destroy(gameObject, 3f);
    }
    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        _rb.MovePosition(transform.position + _direccionDisparo * _speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(8);
            }
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}