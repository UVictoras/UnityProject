using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseAttack : MonoBehaviour
{
    public bool _isHitting;
    static public float _damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHitting)
        {
            _damage = 10;
        }
    }

   
}
