using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public string _playerOneCharacter;
    [HideInInspector]
    public string _playerTwoCharacter;
    [HideInInspector]
    public string _mapChoice;

    public static GameManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
            Destroy(gameObject);
    }
}
