using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;


//Ataque moviendose y sin moverse
//si esta quieto ataca y no puede moverse durante el ataque
//solo es necesario el debug diciendo que ha atacado al pulsar el boton, pero si haces mas, pues god

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    //en esta nueva version necesitas poner input actions para hacer movimiento y tal
    private InputAction _moveAction;
    private Vector2 _moveInput;
    private InputAction _jumpAction;
    private InputAction _atackAction;
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
        foreach (Collider2D Star in interactables)
        {
            if (Star.gameObject.tag == "Star")
            {
                Debug.Log("Tocando estrella");
            }
        }
        Debug.Log("Cositas");
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

    }

    void Movement()
    {
        if (_moveInput.x > 0)
        {
            _animator.SetBool("IsRunning", true);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_moveInput.x < 0)
        {
            _animator.SetBool("IsRunning", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }

    }

}

