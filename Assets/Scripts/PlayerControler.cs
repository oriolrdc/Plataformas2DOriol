using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//Ataque moviendose y sin moverse
//si esta quieto ataca y no puede moverse durante el ataque
//Patron Singleton, sirve para acceder de forma sencilla a objetos en tu escena, y evita que haya duplicados de ese objeto.

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    //en esta nueva version necesitas poner input actions para hacer movimiento y tal
    private InputAction _moveAction;
    private Vector2 _moveInput;
    private InputAction _jumpAction;
    private InputAction _InteractAction;
    [SerializeField] private float _playerVelocity = 5;
    [SerializeField] private float _jumpForce = 3;
    [SerializeField] private float _JumpHeight = 2;
    [SerializeField] private bool _alreadyLanded = true;
    //public GroundSensor _groundSensor;
    //Ground sensor pro ;P
    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private Vector2 _sensorSize = new Vector2(0.5f, 0.5f);
    //Animaciones
    private Animator _animator;
    //Interact
    [SerializeField] private Vector2 _interactionZone = new Vector2(1, 1);
    //Atack
    private InputAction _atackAction;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private float _attackRange = 1;
    [SerializeField] private float _attackDamage = 10;
    [SerializeField] private bool _isRunning = false;
    //Vida
    [SerializeField] private float _maxHeatlh = 40;
    [SerializeField] private float _currentHealth;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //Esto de abajo es un getcomponent para inputs, los dos son correctos pero mejor usar el de arriba(Move).
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _atackAction = InputSystem.actions["Attack"];
        _InteractAction = InputSystem.actions["Interact"];
        //_atackAction = InputSystem.actions.FindAction("Atack"); (Version 2 de lo que comentaba arriba).
        //_groundSensor = GetComponentInChildren<GroundSensor>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _currentHealth = _maxHeatlh;
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        //transform.position = transform.position + new Vector3(_moveInput.x, 0, 0) * _playerVelocity * Time.deltaTime;

        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        if (_InteractAction.WasPerformedThisFrame())
        {
            Interact();
        }

        Movement();

        _animator.SetBool("IsJumping", !IsGrounded());

        if (_atackAction.WasPressedThisFrame())
        {
            _animator.SetTrigger("Attack");
            Attack();
        }
        else if(_atackAction.WasPressedThisFrame() && _isRunning)
        {
            _animator.SetTrigger("MavingAttack");
            Attack();
        }
    }

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_playerVelocity * _moveInput.x, _rigidbody.linearVelocity.y);
    }

    void Jump()
    {
        _rigidbody.AddForce(transform.up * Mathf.Sqrt(_JumpHeight * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);
    }

    void Interact()
    {
        Collider2D[] interactables = Physics2D.OverlapBoxAll(transform.position, _interactionZone, 0);
        foreach (Collider2D item in interactables)
        {
            if (item.gameObject.tag == "Star")
            {
                Star starScript = item.gameObject.GetComponent<Star>();

                if (starScript != null)
                {
                    starScript.Interaction();
                }
            }
        }
    }

    bool IsGrounded()
    {
        Collider2D[] ground = Physics2D.OverlapBoxAll(_sensorPosition.position, _sensorSize, 0);
        foreach (Collider2D item in ground)
        {
            if (item.gameObject.layer == 3)
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, _interactionZone);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);

    }

    void Movement()
    {
        if (_moveInput.x > 0)
        {
            _isRunning = true;
            _animator.SetBool("IsRunning", true);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_moveInput.x < 0)
        {
            _isRunning = true;
            _animator.SetBool("IsRunning", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _isRunning = false;
            _animator.SetBool("IsRunning", false);
        }

    }

    /*public void TakeDamage(int damage)
    {
        _playerHealth -= damage;
    }*/

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        float health = _currentHealth / _maxHeatlh;
        Debug.Log(health);
        GUI.Instance.UpdateHealthBar(_currentHealth, _maxHeatlh);

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void Attack()
    {
        Collider2D[] Enemy = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRange, _enemyLayer);
        foreach (Collider2D item in Enemy)
        {
            if (item.gameObject.layer == 6)
            {
                EnemyController _enemyScript = item.gameObject.GetComponent<EnemyController>();
                _enemyScript.TakeDamage(_attackDamage);
            }
        }
    }
        

}

