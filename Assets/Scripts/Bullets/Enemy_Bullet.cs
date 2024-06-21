using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 30f;
    private Rigidbody2D _rb;
    private Vector3 _direction;
    readonly WaitForSeconds delay = new WaitForSeconds(2);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _rb.velocity = _direction * _speed;
        StartCoroutine(DisableAfterTime());
    }
    public void Initialize(Vector3 direction)
    {
        _direction = direction.normalized;
        _rb.velocity = _direction * _speed;
    }

    private IEnumerator DisableAfterTime()
    {
        yield return delay;
        gameObject.SetActive(false);
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
            gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}