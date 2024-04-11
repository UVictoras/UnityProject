using UnityEngine;
using Unity;
using TMPro;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

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
    [HideInInspector]
    public string _direction;

    public int _jumpsLeft;
    public int _lifesRemaining;
    public float _jumpForce;
    public float _percentage;
    public string _name;
    public string _characterName;
    public GameObject _bullet;
    public TextMeshProUGUI _textName;
    public GameObject _baseAttack;
    public Transform _shootingPoint;
    public Animator _animator;

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
        _direction = "right";
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
        _textName.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        if (horizontalInput < 0 )
            _direction = "left";
        else if (horizontalInput > 0 ) 
            _direction = "right";
        transform.rotation = Quaternion.Euler(0, _direction == "left"? 180 : 0, 0);
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
            _baseAttack.SetActive(true);
            if (_baseAttack.GetComponent<Collider2D>().isTrigger)
                PlayerBaseAttack._isHitting = true;
        }
        if (Input.GetKeyUp(_playerId == 1 ? KeyCode.E : KeyCode.Keypad2))
        {
            _animator.SetBool("isPressed", false);
            _baseAttack.SetActive(false);
            PlayerBaseAttack._isHitting = false;
        }
    }

    private void UpdateUI()
    {
        _percentageText.text = _percentage.ToString() + "%";
        _textName.text = _name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.tag == "attack" || collision.tag == "Axe" || collision.tag == "Player" || collision.tag == "specialAttack")
        {
            GameObject attack = collision.gameObject;
            if (attack.GetType() == typeof(Bullet))
            if (attack.tag == "Axe")
            {
                //_animator.SetTrigger("TakeDamage");
                Vector2 pushBack = new Vector2((_percentage * attack.GetComponent<Bullet>()._strenght * attack.transform.localPosition.x) * 2, (_percentage * 1 / attack.GetComponent<Bullet>()._strenght * attack.transform.localPosition.y) * 2);
                _percentage += attack.GetComponent<Bullet>()._damage;
                _body.AddForce(pushBack);
            }
            else if (attack.tag == "attack")
            {
                //_animator.SetTrigger("TakeDamage");
                Vector2 pushBack = new Vector2((_percentage *  attack.GetComponent<PlayerBaseAttack>()._strenght * attack.transform.localPosition.x) * 2, (_percentage * attack.GetComponent<PlayerBaseAttack>()._strenght * attack.transform.localPosition.y) * 2);
                _percentage += attack.GetComponent<PlayerBaseAttack>()._damage;
                _body.AddForce(pushBack);
            }
            else if (attack.tag == "Player") 
            {
                if (attack.GetComponent<SpecialAttackOdin>() != null)
                {
                    if (attack.GetComponent<SpecialAttackOdin>()._isActive)
                    {
                        Vector2 pushBack = new Vector2((_percentage * attack.GetComponent<SpecialAttackOdin>()._strenght * attack.transform.localPosition.x) * 2, (_percentage * attack.GetComponent<SpecialAttackOdin>()._strenght * attack.transform.localPosition.y) * 2);
                        _percentage += attack.GetComponent<SpecialAttackOdin>()._damage;
                        _body.AddForce(pushBack);
                    }
                }
            }
            
        }
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
