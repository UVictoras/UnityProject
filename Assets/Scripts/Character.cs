using UnityEngine;
using Unity;
using TMPro;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System.Collections;

public class Character : MonoBehaviour
{
    /* ----------------------------------------------------- *\
    |                                                         |
    |                          Field                          |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Field

    public float _speed;
    [SerializeField]
    private GameObject _deathParticles;

    private int _maxJumps;
    private Rigidbody2D _body;

    [HideInInspector]
    public int _playerId;
    [HideInInspector]
    public bool _isGrounded;
    [HideInInspector]
    public List<GameObject> _hearts;
    [HideInInspector]
    public TextMeshProUGUI _percentageText;

    public int _jumpsLeft;
    public int _lifesRemaining;
    public float _jumpForce;
    public float _percentage;
    public string _name;
    public GameObject _bullet;
    public GameObject baseAttack;
    public Transform _shootingPoint;
    public Animator _animator;


    private bool _timerRunning = false;
    #endregion Field

    /* ----------------------------------------------------- *\
    |                                                         |
    |                         Methods                         |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _maxJumps = 2;
        _jumpsLeft = _maxJumps;
        _percentage = 24;
        _lifesRemaining = 3;
        _hearts = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            int offset = _playerId == 1 ? 0 : 3;
            _hearts.Add(Main.instance._hearts[i + offset]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Shoot();
        HandleBaseAttack();
        UpdateUI();
    }

    private void Move()
    {
        float horizontalInput = _playerId == 1 ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal2");
        Vector3 movement = new Vector3(horizontalInput * _speed * Time.deltaTime, 0.0f, 0.0f);
        transform.position += movement;
        transform.rotation = Quaternion.Euler(0, horizontalInput < 0 ? 180 : 0, 0);
    }

    private void Jump()
    {
        if (_isGrounded && _body.velocity.y <= 0)
            _jumpsLeft = _maxJumps;

        if (Input.GetButtonDown(_playerId == 1 ? "Jump" : "Jump2") && (_jumpsLeft > 0 || _isGrounded))
        {
            if (_jumpsLeft == 1)
                _body.velocity = new Vector2(_body.velocity.x, 0.0f);

            _body.AddForce(Vector2.up * _jumpForce * 6.0f);
            _jumpsLeft--;
        }
    }

    private void Shoot()
    {
        if (Input.GetButtonDown(_playerId == 1 ? "Fire1" : "Fire2"))
            Instantiate(_bullet, _shootingPoint.position, transform.rotation);
    }

    private void HandleBaseAttack()
    {
        if (Input.GetKeyDown(_playerId == 1 ? KeyCode.E : KeyCode.Keypad2))
        {
            _animator.SetBool("isPressed", true);
            baseAttack.SetActive(true);
            if (baseAttack.GetComponent<Collider2D>().isTrigger)
                PlayerBaseAttack._isHitting = true;
        }
        if (Input.GetKeyUp(_playerId == 1 ? KeyCode.E : KeyCode.Keypad2))
        {
            _animator.SetBool("isPressed", false);
            baseAttack.SetActive(false);
            PlayerBaseAttack._isHitting = false;
        }
    }

    private void UpdateUI()
    {
        _percentageText.text = _percentage.ToString() + "%";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "attack")
        {
            GameObject attack = collision.gameObject;
            if (attack.GetType() == typeof(Bullet))
            {
                //_animator.SetTrigger("TakeDamage");
                Vector2 pushBack = new Vector2(_percentage * (1 / attack.GetComponent<Bullet>()._strenght) * attack.transform.localPosition.x, _percentage * (1 / attack.GetComponent<Bullet>()._strenght) * attack.transform.localPosition.y);
                _percentage += attack.GetComponent<Bullet>()._damage;
                _body.AddForce(pushBack);
            }
            else if (attack.GetType() == typeof(PlayerBaseAttack))
            {
                //_animator.SetTrigger("TakeDamage");
                Vector2 pushBack = new Vector2(_percentage * (1 / attack.GetComponent<PlayerBaseAttack>()._strenght) * attack.transform.localPosition.x, _percentage * (1 / attack.GetComponent<PlayerBaseAttack>()._strenght) * attack.transform.localPosition.y);
                _percentage += attack.GetComponent<PlayerBaseAttack>()._damage;
                _body.AddForce(pushBack);
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ZeusWrath" && this == ZeusWrath.instance._enemy)
        {
            if (_timerRunning == false)
            {
                _percentage += 10;
                StartCoroutine(Timer());
            }
                
        }

    }

    public IEnumerator Timer()
    {
        _timerRunning = true;
        yield return new WaitForSeconds(1);
        _timerRunning = false;


    }
    private void OnBecameInvisible()
    {
        _hearts[_lifesRemaining - 1].gameObject.SetActive(false);
        _lifesRemaining--;
        _percentage = 0;

        Quaternion dir = new Quaternion(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y, gameObject.GetComponent<Rigidbody2D>().velocity.y, gameObject.GetComponent<Rigidbody2D>().velocity.x);
        Instantiate(_deathParticles, transform.position, dir);

        transform.position = new Vector3(0, 0, 0);

        if (_lifesRemaining == 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion Methods
}
