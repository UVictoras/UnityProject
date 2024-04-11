using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    /* ----------------------------------------------------- *\
    |                                                         |
    |                          Field                          |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Field

    private AudioSource _audioSource;

    public Transform _destination;
    public float _distance;
    public string _direction;

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
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Vector2.Distance(transform.position, collision.transform.position) > _distance)
        {
            Vector2 newPosition = new Vector2(_destination.position.x + (_direction == "left" ? 1f : -1f), _destination.position.y);
            collision.transform.position = newPosition;
            _audioSource.Play();
        }
    }

    #endregion Methods
}
