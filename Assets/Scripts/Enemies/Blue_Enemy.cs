using System.Collections;
using TMPro;
using UnityEngine;

public class Blue_Enemy : Enemy
{
    private bool isPlayerDetected;
    private float _minSpeed = 2f;
    private float _maxSpeed = 18f;
    private float _SpeedReduction = 12f;

    void Start()
    {
        distanciaMinima = 0.5f;
        _damage = 5;
    }
    protected override void Update()
    {
        PlayerDodge();
        RecoverSpeed(ref _maxSpeed);
    }
    void PlayerDodge()
    {
        if (isPlayerDetected && Input.GetKeyDown(KeyCode.Space))
        {
            isPlayerDetected = false;
            transform.position -= (player.transform.position - transform.position);
            // Reducción de velocidad asegurando que no baje de _minSpeed
            _speed = Mathf.Max(_minSpeed, _speed - _SpeedReduction);
        }
    }

    private void RecoverSpeed(ref float speed)
    {
        if (_speed < _maxSpeed)
        {
            _speed += speed / _speed * Time.deltaTime;
        }
    }
    private IEnumerator ApplyDamageRepeatedly(GameObject player)
    {
        while (isPlayerDetected)
        {
            ApplyDamageToPlayer(player);
            yield return new WaitForSeconds(1f); // Aplica el daño cada segundo
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet_Gauss") && !isPlayerDetected)
        {
            Die();
        }
        if (collision.collider.CompareTag("Player"))
        {
            _enemyRb.constraints = RigidbodyConstraints2D.FreezeAll;
            isPlayerDetected = true;
            _playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(ApplyDamageRepeatedly(collision.collider.gameObject));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerDetected = false;
            _playerRb.constraints = RigidbodyConstraints2D.None;
            _enemyRb.constraints = RigidbodyConstraints2D.None;
        }
    }

}