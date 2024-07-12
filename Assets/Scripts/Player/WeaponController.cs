using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private readonly string[] projectils = { "Bullet_Laser", "Bullet_Gauss", "Bullet_Fire" };
    private byte _currentProjectileIndex;
    private readonly float[] _fireDelays = { 0.1f, 0.4f, 0.3f };
    private float _lastFireTime, _currentFireDelay = 0.1f;
    /*--------------SOUNDS---------------*/
    public AudioClip _laser;
    public AudioClip _gauss;
    public AudioClip _fire;
    public AudioClip _changeWeapon;

    public void ChangeProjectile(byte index)
    {
        if (index >= 0 && index < projectils.Length)
        {
            AudioPooler.Instance.PlaySound(_changeWeapon, transform.position);
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
                    switch (GetProyectilType())
                        {
                    case 0:
                        AudioPooler.Instance.PlaySound(_laser, transform.position);
                        break;
                    case 1:
                        AudioPooler.Instance.PlaySound(_gauss, transform.position);
                        break;
                    case 2:
                        AudioPooler.Instance.PlaySound(_fire, transform.position);
                        break;
                        }
            }
            
            _lastFireTime = Time.time;
        }
    }
    public byte GetProyectilType()
    {
        return _currentProjectileIndex;
    }
}