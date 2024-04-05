using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float _maxHealth;
    private float _health;
    public GameObject _enemy;
    // Start is called before the first frame update
    void Awake()
    {
        _health = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if ( _health <= 0)
        {
            _enemy.SetActive(false);   
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BaseAttack")
        {
            _health -= PlayerBaseAttack._damage;
        }
    }
}
