using System.Collections;
using UnityEngine;

public class Red_Enemy : Enemy
{
    [SerializeField]
    private GameObject weapon;
    private string bullet = "E_RedBullet";
    private float _fireDelay = 1f;
    private bool canFire = false;
    WaitForSeconds delay;

    private void Start()
    {
        delay = new WaitForSeconds(_fireDelay);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        canFire = false;
        StartCoroutine(FireDelay());
    }
    protected override void Attack(float distancia)
    {
        if (Time.time - tiempoUltimoDisparo >= cadenciaDeFuego && distanciaMaxima >= distancia && canFire)
        {
            GameObject newProjectile = ObjectPooler.SharedInstance.GetPooledObjects(bullet);
            if (newProjectile != null)
            {                
                newProjectile.transform.SetPositionAndRotation(transform.position, transform.rotation);

                Enemy_Bullet bulletScript = newProjectile.GetComponent<Enemy_Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.Initialize(transform.right);
                }
                newProjectile.SetActive(true);
            }
            tiempoUltimoDisparo = Time.time;
        }       
    }

    // Delay entre el spawn y el primer disparo
    private IEnumerator FireDelay()
    {        
        yield return delay;
        canFire = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet_Laser"))
        {
            Die();
        }
    }

}