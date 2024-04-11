using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialAttackZeus : MonoBehaviour
{
    public float _damage = 50;
    public float _strenght = 5;
    public GameObject _specialAttack;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

        if (Input.GetButtonDown(_specialAttack.GetComponentInParent<Character>()._playerId == 1 ? "SpecialAttack1" : "SpecialAttack2"))
        {
            _specialAttack.SetActive(true);
            StartCoroutine(Wait());
            _specialAttack.SetActive(false);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
