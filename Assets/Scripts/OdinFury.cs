using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OdinFury : MonoBehaviour
{
    /* ----------------------------------------------------- *\
    |                                                         |
    |                          Field                          |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Field

    private bool _isActive;
    private GameObject _itsASystemInsideOfUnity;

    public GameObject _myOnlySunshine;
    public GameObject _unityParticleSystem;
    public static OdinFury instance;

    [SerializeField]
    private int _boostSpeed;

    private Character _enemy;
    private bool _timerRunning = false;
    #endregion Field

    /* ----------------------------------------------------- *\
    |                                                         |
    |                         Methods                         |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Methods

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (_isActive == false)
            return;


        if (_timerRunning == false)
        {
            StartCoroutine(Timer());
            _enemy._percentage += 1;
        }


    }


    public IEnumerator Timer()
    {
        _timerRunning = true;
        yield return new WaitForSeconds(1);
        _timerRunning = false;

    }

    public void Begin(Character player, Character enemy)
    {
        if (_isActive == true)
            return;

        _isActive = true;
        _enemy = enemy;
        _myOnlySunshine.GetComponent<Light2D>().color = new Color(0.13333333333f, 0.0f, 0.0f, 0.5f);

        _itsASystemInsideOfUnity = Instantiate(_unityParticleSystem, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
        _itsASystemInsideOfUnity.transform.localScale *= 25.0f;


        player._speed *= _boostSpeed;

    }

    public void Disable(Character player)
    {
        if (_isActive == false)
            return;

        _isActive = false;

        _myOnlySunshine.GetComponent<Light2D>().color = Color.white;


        
        player._speed /= _boostSpeed;

        Destroy(_itsASystemInsideOfUnity);
    }

    #endregion Methods
}
