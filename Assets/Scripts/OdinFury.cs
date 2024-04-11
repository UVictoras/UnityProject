using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEditor.Experimental.GraphView.GraphView;

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


    }

    public void Begin(GameObject[] players)
    {
        if (_isActive == true)
            return;

        _isActive = true;

        _myOnlySunshine.GetComponent<Light2D>().color = new Color(0.13333333333f, 0.0f, 0.0f, 0.5f);

        _itsASystemInsideOfUnity = Instantiate(_unityParticleSystem, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
        _itsASystemInsideOfUnity.transform.localScale *= 25.0f;


        for(int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Character>()._speed *= _boostSpeed;
        }

    }

    public void Disable(GameObject[] players)
    {
        if (_isActive == false)
            return;

        _isActive = false;

        _myOnlySunshine.GetComponent<Light2D>().color = Color.white;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Character>()._speed /= _boostSpeed;
        }

        Destroy(_itsASystemInsideOfUnity);
    }

    #endregion Methods
}
