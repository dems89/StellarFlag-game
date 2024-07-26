using System.Collections;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    /*--------------Referencias---------------*/
    protected GameObject player;
    protected Rigidbody2D _enemyRb, _playerRb;
    protected Collider2D _enemyCollider;
    /*--------------Propiedades del Enemy---------------*/
    //[SerializeField]
    protected float _speed = 13f;
    protected float distanciaMaxima = 30f, distanciaMinima = 15f, cadenciaDeFuego = 2f, tiempoUltimoDisparo;
    protected sbyte _damage;
    protected Animator _anim;
    protected bool isAlive;
    [SerializeField]
    protected bool isAnTestEnemy = false;
    [SerializeField]
    private AudioClip _die;

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
        if (player != null && isAlive && !isAnTestEnemy)
        {
            Attack(ObjetiveDistance());
        }      
    }
    protected virtual void FixedUpdate()
    {
        if (player != null && isAlive)
        {
            LookAtPlayer();
            if (ObjetiveDistance() <= distanciaMaxima && ObjetiveDistance() >= distanciaMinima && !isAnTestEnemy)
            {

                Movement();
            }            
        }        
    }
    protected virtual void OnEnable()
    {
        isAlive = true;
        _enemyCollider.enabled = true;        
    }
    public float ObjetiveDistance()
    {
        float distancia = Vector2.Distance(transform.position, player.transform.position);
        
        return distancia;
    }

    protected virtual void Attack(float distancia)
    {
    }
    private void Movement()
    {
        Vector3 direction = ( player.transform.position - transform.position).normalized;
        Vector3 targetPosition = transform.position + direction * _speed * Time.fixedDeltaTime;
        _enemyRb.MovePosition(targetPosition);
    }
    private void LookAtPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
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
        GameManager.Instance.UpdateEnemyCount(gameObject.tag);
        if (_anim != null)
        {
            AudioPooler.Instance.PlaySound(_die, transform.position);
            _enemyCollider.enabled = false;
            _anim.SetTrigger("Die");           
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            gameObject.SetActive(false);
        }        
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length); // Esperar hasta que termine la animación de muerte
        gameObject.SetActive(false);
    }
}
        


