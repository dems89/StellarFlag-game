using System.Collections;
using UnityEngine;

public class Green_Enemy : Enemy
{
    [SerializeField]
    private GameObject _greenBullet;
    public Transform crosshair;
    public float rayMaxLength = 10f, attackCooldown = 2f;
    public LayerMask capaObstaculo;
    private Vector2 _lastPlayerPosition;
    public LineRenderer lineRenderer;

    void Start()
    {
        distanciaMinima -= 3f;
        cadenciaDeFuego += 0.5f;
        _speed -= 2f;
        lineRenderer.enabled = false;
        _damage = 20;
    }


    protected override void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (player != null && ObjetiveDistance() <= distanciaMinima  && attackCooldown <= 0.0f && isAlive)
        {            
            _lastPlayerPosition = player.transform.position;
            lineRenderer.enabled = false;
            StartCoroutine(EmitRay());
            attackCooldown = cadenciaDeFuego;           
        }
    }
    private IEnumerator EmitRay()
    {
        _enemyRb.constraints = RigidbodyConstraints2D.FreezeAll;
        Vector3 direction = (_lastPlayerPosition - (Vector2)crosshair.position).normalized;
        yield return new WaitForSeconds(.3f);
        RaycastHit2D hit = Physics2D.Raycast(crosshair.position, direction, rayMaxLength, capaObstaculo);     
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, crosshair.position);

        if (hit.collider != null)
        {
            lineRenderer.SetPosition(1, hit.point);
            //Debug.Log("Rayo golpeó: " + hit.collider.name);                                                                         

            if (hit.collider.CompareTag("Player"))
            {             
                ApplyDamageToPlayer(hit.collider.gameObject);
            }
        }
        else
        {            
                Vector3 endPosition = crosshair.position + direction * rayMaxLength;
                lineRenderer.SetPosition(1, endPosition);
                //Debug.Log("Rayo no golpea con nada");            

        }
        yield return new WaitForSeconds(0.4f); //Espera para desaparecer el LineRenderer
        _enemyRb.constraints = RigidbodyConstraints2D.None;
        lineRenderer.enabled = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colision detectada" + collision.collider.name);
        if (collision.gameObject.CompareTag("Bullet_Fire"))
        {
            Die();
        }
    }
}