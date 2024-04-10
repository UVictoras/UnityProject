using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseAttack : MonoBehaviour
{
    /* ----------------------------------------------------- *\
    |                                                         |
    |                          Field                          |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Field

    static public bool _isHitting = false;
    public float _damage = 10;
    public float _strenght = 2;

    #endregion Field

    /* ----------------------------------------------------- *\
    |                                                         |
    |                         Methods                         |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Method
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

        _isHitting = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isHitting = false;
    }

    #endregion Method
}
