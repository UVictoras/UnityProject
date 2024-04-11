using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class SpecialAttackOdin : MonoBehaviour
{
    public float _damage = 20;
    public float _strenght = 50;
    public float _strenghtDash = 12;
    public Rigidbody2D _body;
    public Transform _selfTransform;
    public bool _isActive = false;
    public float _dashingTime = 1.5f;
   
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        
        if (Input.GetButtonDown(_selfTransform.GetComponentInParent<Character>()._playerId == 1 ? "SpecialAttack1" : "SpecialAttack2"))
        {
            _body.velocity = new Vector2(_selfTransform.GetComponentInParent<Character>()._direction == "right" ? _strenghtDash : -_strenghtDash, 0);
            _isActive = true;
            StartCoroutine(WaitIsActive());
            _isActive = false;
        }
    }

    private IEnumerator WaitIsActive()
    {
        yield return new WaitForSeconds(_dashingTime);
        print("i");
    }
}


