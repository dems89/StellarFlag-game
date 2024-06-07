using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*--------------Referencias---------------*/
    protected GameObject player;
    protected Rigidbody2D _enemyRb, _playerRb;
    protected Collider2D _enemyCollider;
    [SerializeField]
    /*--------------Propiedades del Enemy---------------*/
    protected float _speed = 10f;
    protected float distanciaMaxima = 30f, distanciaMinima = 10f, cadenciaDeFuego = 2f, tiempoUltimoDisparo;
    protected sbyte _damage;
    protected Animator _anim;
    protected bool isAlive = true;

    protected virtual void Awake()
    {
        player = GameObject.FindWithTag("Player");
        _enemyRb = gameObject.GetComponent<Rigidbody2D>();
        _playerRb = player.GetComponent<Rigidbody2D>();
        _enemyCollider = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();
        
    }

    protected virtual void Update()
    {
        if (player != null && isAlive)
        {
            Attack(ObjetiveDistance());
        }
    }
    protected virtual void FixedUpdate()
    {
        if (player != null && isAlive)
        {
            LookAtPlayer(player.transform);
            if (ObjetiveDistance() <= distanciaMaxima && ObjetiveDistance() >= distanciaMinima)
            {

                Movement(player.transform.position);
            }            
        }        
    }
    public float ObjetiveDistance()
    {
        float distancia = Vector2.Distance(transform.position, player.transform.position);
        
        return distancia;
    }

    protected virtual void Attack(float distancia)
    {
    }
    private void Movement(Vector2 playerPosition)
    {
        Vector2 direction = (playerPosition - _enemyRb.position).normalized;
        Vector2 targetPosition = _enemyRb.position + direction * _speed * Time.fixedDeltaTime;
        _enemyRb.MovePosition(targetPosition);
    }
    private void LookAtPlayer(Transform player)
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        _enemyRb.MoveRotation(targetAngle);
    }
    protected void ApplyDamageToPlayer(GameObject player)
    {
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }
    }
    protected virtual void Die()
    {
        isAlive = false;
        if (_anim != null)
        {
            _enemyCollider.enabled = false;
            _anim.SetTrigger("Die");           
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length); // Esperar hasta que termine la animación de muerte
        Destroy(gameObject);
    }
}
        


