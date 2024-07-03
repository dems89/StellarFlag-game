using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _speed = 900f;
    private Rigidbody2D rb;
    private WeaponController weaponController;
    [SerializeField]
    private int _maxHealth = 100;
    private int _currentHealth;
    private Vector2 _lastCeckPoint;
    private bool _canShoot = true, _isShieldActive = false;
    private Collider2D _playerCollider;
    [SerializeField]
    private GameObject _shieldreflect;    
    /*--------------ANIMATIONS---------------*/
    private Animator _animControl;
    private Animator _shieldAnimator;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<Collider2D>();
        weaponController = GetComponentInChildren<WeaponController>();
        _animControl = GetComponent<Animator>();
        _lastCeckPoint = transform.position;
        _shieldAnimator = _shieldreflect.GetComponent<Animator>();
    }
    void Start()
    {        
        _currentHealth = _maxHealth;
        SetHUDMaxHealth();
        UpdateHUDHealth();

    }
    void Update()
    {
        if (_playerCollider.enabled != false)
        {
            Shoot();
            WeaponChange();
            if (Input.GetKeyDown(KeyCode.Space) && !_isShieldActive)
            {
                UseShield();
            }
        }
    }
    void FixedUpdate()
    {
        if (_playerCollider.enabled != false) 
        {
            Movement();
            LookAtCursor();
        }
        
    }

    /*--------------Mecanicas---------------*/
    void Movement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        //movement.Normalize(); //Quito el normalizado porque se mueve por cuadrante
        rb.velocity = _speed * movement * Time.fixedDeltaTime;
    }

    private void LookAtCursor()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    private void Shoot()
    {
        if (!_canShoot)
        {
            return;
        }
        else if(Input.GetButtonDown("Fire1") && weaponController != null)
        {           
                weaponController.FireProjectile();
        }
        
    }
    private void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponController.ChangeProjectile(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponController.ChangeProjectile(1); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponController.ChangeProjectile(2); 
        }

    }

    private void UseShield()
    {
        _isShieldActive = true;
        _shieldreflect.gameObject.SetActive(true);
        _shieldAnimator.SetTrigger("ActiveShield");
        StartCoroutine(WaitAnimationEnd());
    }


    public void TakeDamage(short damage)
    {
        if (_currentHealth <= damage)
        {
            if (_animControl != null)
            {
                _playerCollider.enabled = false;
                _animControl.SetBool("IsDestroyed", true);
                StartCoroutine(RespawnAfterAnimation());
            }         
        }
        else
        {
            _currentHealth -= damage;
            UpdateHUDHealth();
            //Debug.Log(_currentHealth);
        }
    }

    public void ChangeShootCap(bool state)
    {
        _canShoot = state;
    }
  
    /*--------------INTERFACES---------------*/

    private IEnumerator RespawnAfterAnimation()
    {
        yield return new WaitForSeconds(_animControl.GetCurrentAnimatorStateInfo(0).length);
        Respawn();
    }
    private IEnumerator WaitAnimationEnd()
    {
        yield return new WaitForSeconds(_shieldAnimator.GetCurrentAnimatorStateInfo(0).length);
        _shieldreflect.gameObject.SetActive(false);
        _isShieldActive = false;
    }

    /*--------------UPDATE-HUD-HEALTH--------------*/

    private void UpdateHUDHealth()
    {
        if (HUDManager.Instance != null)
        {
            HUDManager.Instance.UpdateUIplayerHealth(_currentHealth);
        }
    }
    private void SetHUDMaxHealth()
    {
        if (HUDManager.Instance != null)
        {
            HUDManager.Instance.SetUImaxPlayerHealth(_maxHealth);
        }
    }
    private void Respawn()
    {
        if (LifeManager.Instance != null && LifeManager.Instance.IsAlive())
        {
            LifeManager.Instance.DecreaseLife();
            transform.position = _lastCeckPoint;
            _currentHealth = _maxHealth;
             UpdateHUDHealth();
            _playerCollider.enabled = true;
            _animControl.SetBool("IsDestroyed", false);                         
        }else if (HUDManager.Instance != null && !LifeManager.Instance.IsAlive())
        {
            HUDManager.Instance.SetHUD(HUDType.Defeat);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Vector2 checkpoint = collision.transform.position;
            _lastCeckPoint = checkpoint;
        }      
    }
}


