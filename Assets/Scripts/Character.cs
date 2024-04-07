using UnityEngine;
using Unity;
using TMPro;

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

    [HideInInspector]
    public int _playerId;
    public Transform _shootingPoint;
    public GameObject _bullet;
    public GameObject baseAttack;
    public Animator _animator;
    [HideInInspector]
    public bool _isGrounded;
    public int _jumpsLeft;

    private int _percentage;
    private int _lifesRemaining;
    [HideInInspector]
    public TextMeshProUGUI _percentageText;

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
        _percentage = 24;
        _lifesRemaining = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGrounded == true && _body.velocity.y <= 0)
        {
            _jumpsLeft = _maxJumps;
        }

        if (_playerId == 1)
        {
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
        }

        if (_playerId == 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.GetComponent<Transform>().localPosition += new Vector3(-Time.deltaTime * _speed, 0.0f, 0.0f);
                GetComponent<Transform>().localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.GetComponent<Transform>().localPosition += new Vector3(Time.deltaTime * _speed, 0.0f, 0.0f);
                GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && (_jumpsLeft > 0 || _isGrounded == true))
            {
                if (_jumpsLeft == 1)
                {
                    _body.velocity = new Vector2(_body.velocity.x, 0.0f);
                }
                _body.AddForce(new Vector2(0, _jumpForce * 6.0f));
                _jumpsLeft--;
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                Instantiate(_bullet, _shootingPoint.position, transform.rotation);
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetBool("isPressed", true);
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

        _percentageText.GetComponent<TextMeshProUGUI>().text = _percentage.ToString() + "%";
    }

    #endregion Methods
}
