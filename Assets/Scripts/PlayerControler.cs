using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    //en esta nueva version necesitas poner input actions para hacer movimiento y tal
    [SerializeField] private InputAction _moveAction;
    [SerializeField] private Vector2 _moveInput;
    [SerializeField] private InputAction _jumpAction;
    [SerializeField] private InputAction _atackAction;
    [SerializeField] private float _playerVelocity = 5;
    [SerializeField] private float _jumpForce = 3;
    [SerializeField] private float _JumpHeight = 2;
    //public GroundSensor _groundSensor;

    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private Vector2 _sensorSize = new Vector2(0.5f, 0.5f);

    //Animaciones
    [SerializeField] private Animator _animator;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //Esto de abajo es un getcomponent para inputs, los dos son correctos pero mejor usar el de arriba(Move).
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _atackAction = InputSystem.actions["Attack"];
        //_atackAction = InputSystem.actions.FindAction("Atack"); (Version 2 de lo que comentaba arriba).
        //_groundSensor = GetComponentInChildren<GroundSensor>();
        _animator = GetComponent<Animator>();

    }

    void Start()
    {

    }

    void Update()
    {
        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        Movement();

        _moveInput = _moveAction.ReadValue<Vector2>();
        Debug.Log(_moveInput);

        //transform.position = transform.position + new Vector3(_moveInput.x, 0, 0) * _playerVelocity * Time.deltaTime;

        _animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_playerVelocity * _moveInput.x, _rigidbody.linearVelocity.y);
    }

    void Jump()
    {
        _animator.SetBool("IsJumping", true);
        _rigidbody.AddForce(transform.up * Mathf.Sqrt(_JumpHeight * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);
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

