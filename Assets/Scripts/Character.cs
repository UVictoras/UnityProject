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

    [SerializeField]
    private GameObject _platform;

    private Rigidbody2D _body;
    private int _jumpsLeft;
    private int _maxJumps;

    public float _jumpForce;

    public Transform shootingPoint;
    public GameObject bullet;

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
        if (IsGrounded() && _body.velocity.y <= 0)
        {
            _jumpsLeft = _maxJumps - 1;
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
        if (Input.GetKeyDown(KeyCode.Space) && (_jumpsLeft > 0 || IsGrounded()))
        {
            _body.velocity = new Vector2(_body.velocity.x, 0);
            _body.AddForce(new Vector2(_body.velocity.x, _jumpForce * 6.0f));
            _jumpsLeft--;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(bullet, shootingPoint.position, transform.rotation);
        }
    }

    bool IsGrounded()
    {
        Collider2D collider = GetComponent<Collider2D>();

        ContactFilter2D contactFilter = new ContactFilter2D();

        Collider2D[] results = new Collider2D[1];

        int count = collider.OverlapCollider(contactFilter, results);

        return count > 0;
    }

    #endregion Methods
}
