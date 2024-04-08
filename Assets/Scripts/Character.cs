using UnityEngine;
using Unity;
using TMPro;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    /* ----------------------------------------------------- *\
    |                                                         |
    |                          Field                          |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Field

    [SerializeField]
    private float _speed;

    private int _maxJumps;
    private int _percentage;
    private int _lifesRemaining;
    private Rigidbody2D _body;

    [HideInInspector]
    public int _playerId;
    [HideInInspector]
    public List<GameObject> _hearts;
    [HideInInspector]
    public bool _isGrounded;
    [HideInInspector]
    public TextMeshProUGUI _percentageText;

    public int _jumpsLeft;
    public float _jumpForce;
    public GameObject _bullet;
    public GameObject baseAttack;
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

    private void OnBecameInvisible()
    {
        _hearts[_lifesRemaining - 1].gameObject.SetActive(false);
        _lifesRemaining--;
        _percentage = 0;

        transform.position = new Vector3(0, 0, 0);

        if (_lifesRemaining == 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion Methods
}
