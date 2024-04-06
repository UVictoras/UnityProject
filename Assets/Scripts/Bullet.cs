using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidBody;
    private int _lifeTime;
    private bool _canCycle;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.right * speed;
        _lifeTime = 5;
        _canCycle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Axe")
            gameObject.transform.Rotate(0.0f, 0.0f, 360.0f * -Time.deltaTime);

        if (_lifeTime == 0)
        {
            Destroy(gameObject);
        }
        if (_canCycle == false)
            return;

        StartCoroutine(CycleLife());
    }

    IEnumerator CycleLife()
    {
        _canCycle = false;

        yield return new WaitForSeconds(1);

        _lifeTime--;

        _canCycle = true;
    }
}
