
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

public class ZeusWrath : MonoBehaviour
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
    public static ZeusWrath instance;


    public Character _enemy;
    private Transform _transformActually;
    private Transform _randomPoint;
    private bool _timerRunning = false;

    [SerializeField]
    private GameObject _thunder;

    [SerializeField]
    private Transform[] spawnPoints;

    private GameObject _instantied;

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


        if(_timerRunning == false)
        {

            do
            {
                _randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            } while (_randomPoint == _transformActually);
            {
                _randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            }
            _instantied = Instantiate(_thunder);
            _instantied.transform.position = _randomPoint.position;
            StartCoroutine(Timer());
            _transformActually = _randomPoint;
            
        }
        
        
    }
    public IEnumerator Timer()
    {
        _timerRunning = true;
        yield return new WaitForSeconds(5);
        Destroy(_instantied);
        _timerRunning = false;

    }
    public void Begin(Character enemy)
    {
        if (_isActive == true)
            return;

        _isActive = true;
        _enemy = enemy;

        //_myOnlySunshine.GetComponent<Light2D>().color = new Color(0.0f, 0.0f, 0.13333333333f, 0.5f);

        _itsASystemInsideOfUnity = Instantiate(_unityParticleSystem, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
        _itsASystemInsideOfUnity.transform.localScale *= 25.0f;
    }

    public void Disable()
    {
        if (_isActive == false)
            return;

        _isActive = false;

        _myOnlySunshine.GetComponent<Light2D>().color = Color.white;
        Destroy(_instantied);
        Destroy(_itsASystemInsideOfUnity);
    }

    #endregion Methods
}
