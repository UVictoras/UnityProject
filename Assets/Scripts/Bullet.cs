using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().Rotate(0, 0, 35);
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
