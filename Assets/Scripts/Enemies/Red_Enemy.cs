using System.Collections;
using UnityEngine;

public class Red_Enemy : Enemy
{
    [SerializeField]
    private GameObject weapon;
    private float _fireDelay = 1f;
    private bool canFire = false;

    private void Start()
    {
        StartCoroutine(FireDelay());
    }
    protected override void Attack(float distancia)
    {
        if (Time.time - tiempoUltimoDisparo >= cadenciaDeFuego && distanciaMaxima >= distancia && canFire)
        {                       
            if (weapon != null)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized; //Esta linea devuelve la dirección del vector. Osea la recta hacia objetivo.
                GameObject newProjectil = Instantiate(weapon, transform.position, transform.rotation);
                if (newProjectil != null)
                {
                    Enemy_Bullet bulletComponent = newProjectil.GetComponent<Enemy_Bullet>();
                    bulletComponent.Configurar(direction);
                }
            }
            tiempoUltimoDisparo = Time.time;
        }       
    }

    // Delay entre el spawn y el primer disparo
    private IEnumerator FireDelay()
    {        
        yield return new WaitForSeconds(_fireDelay);
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