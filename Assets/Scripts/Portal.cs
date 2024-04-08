using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform _destination;
    public float _distance;
    public string _direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Vector2.Distance(transform.position, collision.transform.position) > _distance)
        {
            if (_direction == "left")
            {
                collision.transform.position = new Vector2(_destination.transform.position.x + 1f, _destination.transform.position.y);
            }
            else if (_direction == "up")
            {
                collision.transform.position = new Vector2(_destination.transform.position.x , _destination.transform.position.y - 1f);
            }
            else
            {
                collision.transform.position = new Vector2(_destination.transform.position.x - 1f, _destination.transform.position.y);
            }
            GetComponent<AudioSource>().Play();
        }
    }
}
