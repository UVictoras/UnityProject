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

    [SerializeField] private float _speed;
    private Rigidbody2D _body;
    private float _move;

    public float _jumpForce;
    public bool _isGrounded;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Transform>().localPosition += new Vector3(-Time.deltaTime * _speed, 0.0f, 0.0f);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Transform>().localPosition += new Vector3(Time.deltaTime * _speed, 0.0f, 0.0f);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey(KeyCode.Space)/* && _isGrounded*/)
        {
            gameObject.transform.localPosition += new Vector3(0.0f, _jumpForce * Time.deltaTime, 0.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            _isGrounded = false;
        }
    }

    #endregion Methods
}
