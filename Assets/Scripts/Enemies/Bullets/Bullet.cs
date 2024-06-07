using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 50f;
    private Vector3 _direccionDisparo;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }
    public void Configurar(Vector3 direccion)
    {
        _direccionDisparo = direccion;
    }
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        _rb.MovePosition(transform.position + _direccionDisparo *  _speed * Time.fixedDeltaTime);     
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
      if (!collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
            
        
    }
}