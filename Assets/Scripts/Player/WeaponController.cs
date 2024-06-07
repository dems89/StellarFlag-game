using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject mira;
    [SerializeField] private List<GameObject> projectiles;
    private byte _currentProjectileIndex;
    private readonly float[] _fireDelays = { 0.1f, 0.4f, 0.3f };
    private float _lastFireTime, _currentFireDelay = 0.1f;

    public void ChangeProjectile(byte index)
    {
        if (index >= 0 && index < projectiles.Count)
        {
            _currentProjectileIndex = index;
            _currentFireDelay = _fireDelays[_currentProjectileIndex];
        }
    }

    public void FireProjectile(Quaternion rotation)
    {
        if (Time.time - _lastFireTime >= _currentFireDelay && projectiles.Count > 0)
        {             
                GameObject newProjectile = Instantiate(projectiles[_currentProjectileIndex], transform.position, rotation);
                if (newProjectile != null)
                {
                    if (newProjectile.TryGetComponent<Bullet>(out var bulletComponent))
                    {
                        Vector3 posicionCursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        posicionCursor.z = 0f;
                        Vector3 direccionDisparo = (posicionCursor - transform.position).normalized;
                        bulletComponent.Configurar(direccionDisparo);
                    }
                }
            
            _lastFireTime = Time.time;
        }
    }
}