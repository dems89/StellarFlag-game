using System.Collections;
using UnityEngine;

public class Green_Enemy : Enemy
{
    [SerializeField]
    private Transform crosshair;
    private float rayMaxLength = 10f, attackCooldown = 3f;
    public LayerMask capaObstaculo;
    private Vector2 _lastPlayerPosition;
    public LineRenderer lineRenderer;
    readonly WaitForSeconds delayAiming = new WaitForSeconds(.3f);
    readonly WaitForSeconds delayPostShoot = new WaitForSeconds(.4f);

    void Start()
    {
        distanciaMinima -= 6f;
        cadenciaDeFuego += 0.5f;
        _speed -= 1f;
        lineRenderer.enabled = false;
        _damage = 20;
    }

    protected override void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (player != null && ObjetiveDistance() <= distanciaMinima  && attackCooldown <= 0.0f && isAlive && !isAnTestEnemy)
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
        yield return delayAiming;
        RaycastHit2D hit = Physics2D.Raycast(crosshair.position, direction, rayMaxLength, capaObstaculo);     
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, crosshair.position);

        if (hit.collider != null)
        {
            lineRenderer.SetPosition(1, hit.point);
            //Debug.Log("Rayo golpe�: " + hit.collider.name);                                                                         

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
        yield return delayPostShoot; //Espera para desaparecer el LineRenderer
        _enemyRb.constraints = RigidbodyConstraints2D.None;
        lineRenderer.enabled = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colision detectada" + collision.collider.name);
        if (collision.gameObject.CompareTag("Bullet_Fire"))
        {
            lineRenderer.enabled = false;
            Die();
        }
    }
}