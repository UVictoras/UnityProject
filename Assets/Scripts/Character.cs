using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

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

    private Rigidbody2D _body;
    private int _maxJumps;

    public float _jumpForce;


    public Transform _shootingPoint;
    public GameObject _bullet;
    public GameObject baseAttack;
    public Animator _animator;
    [HideInInspector]
    public bool _isGrounded;
    public int _jumpsLeft;



    #endregion Field

    /* ----------------------------------------------------- *\
    |                                                         |
    |                         Methods                         |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Methods

    // Start is called before the first frame update
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _maxJumps = 2;
        _jumpsLeft = _maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGrounded == true && _body.velocity.y <= 0)
        {
            _jumpsLeft = _maxJumps;
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Transform>().localPosition += new Vector3(-Time.deltaTime * _speed, 0.0f, 0.0f);
            GetComponent<Transform>().localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Transform>().localPosition += new Vector3(Time.deltaTime * _speed, 0.0f, 0.0f);
            GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && (_jumpsLeft > 0 || _isGrounded == true))
        {
            if (_jumpsLeft == 1)
            {
                _body.velocity = new Vector2(_body.velocity.x, 0.0f);
            }
            _body.AddForce(new Vector2(0, _jumpForce * 6.0f));
            _jumpsLeft--;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(_bullet, _shootingPoint.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            _animator.SetBool("isPressed",true);
            baseAttack.SetActive(true);
            if (baseAttack.GetComponent<Collider2D>().isTrigger)
            {
                PlayerBaseAttack._isHitting = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            _animator.SetBool("isPressed", false);
            baseAttack.SetActive(false);
            PlayerBaseAttack._isHitting = false;
        }
    }

    #endregion Methods
}
