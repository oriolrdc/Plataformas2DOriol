using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private PlayerControler _playerControler;
    [SerializeField] private int _enemySpeed = 4;
    private int direction = -1;
    private int _attackDamage = 10;
    private int _EnemyHealth = 20;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerControler = GameObject.Find("Player").GetComponent<PlayerControler>();
    }
    void Start()
    {

    }

    void Update()
    {
        _rigidBody.linearVelocity = new Vector2(_enemySpeed * direction, _rigidBody.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Limit" && direction == 1)
        {
            direction = -1;
        }
        else if (collision.gameObject.tag == "Limit" && direction == -1)
        {
            direction = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _animator.SetTrigger("Attack");
            _playerControler.TakeDamage(_attackDamage);
            //_hasEntered = true;
        }

        if (collision.gameObject.tag == "Player" && direction == -1)
        {
            direction = 1;
        }
        else if (collision.gameObject.tag == "Player" && direction == 1)
        {
            direction = -1;
        }
    }

    /*void OnCollisionExit2D(Collision2D collision)
    {
        _hasEntered = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (_hasEntered == true)
        {
            StartCoroutine(_playerControler.TakeDamage(_attackDamage));
        }
    }*/
}
