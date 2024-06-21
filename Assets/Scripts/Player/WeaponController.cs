using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private readonly string[] projectils = { "Bullet_Laser", "Bullet_Gauss", "Bullet_Fire" };
    private byte _currentProjectileIndex;
    private readonly float[] _fireDelays = { 0.1f, 0.4f, 0.3f };
    private float _lastFireTime, _currentFireDelay = 0.1f;

    public void ChangeProjectile(byte index)
    {
        if (index >= 0 && index < projectils.Length)
        {
            _currentProjectileIndex = index;
            _currentFireDelay = _fireDelays[_currentProjectileIndex];
        }
    }
    public void FireProjectile()
    {
        if (Time.time - _lastFireTime >= _currentFireDelay && projectils.Length > 0)
        {             
                GameObject newProjectil = ObjectPooler.SharedInstance.GetPooledObjects(projectils[_currentProjectileIndex]);
                if (newProjectil != null)
                {
                    newProjectil.SetActive(true);
                    newProjectil.transform.SetPositionAndRotation(transform.position, transform.rotation);
                }
            
            _lastFireTime = Time.time;
        }
    }
}